﻿// <copyright file = "MainViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Events;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Exceptions;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;
using Microsoft.Extensions.Logging;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

public sealed partial class MainViewModel : ObservableRecipient
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasBoundary))]
    private double _canvasHeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasBoundary))]
    private double _canvasWidth;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PlayButtonText))]
    private ShapeViewModel? _selectedShape;

    public MainViewModel()
    {
        this.Localization.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(this.Localization.UiCulture))
            {
                this.OnPropertyChanged(nameof(this.PlayButtonText));
                this.OnPropertyChanged(nameof(this.TriangleButtonText));
                this.OnPropertyChanged(nameof(this.CircleButtonText));
                this.OnPropertyChanged(nameof(this.SquareButtonText));
            }
        };
        this.IntersectionOccured += (_, args) =>
        {
            this.Logger.LogInformation(
                "{FirstShapeName} with {SecondShapeName} @ {Intersection}",
                args.Shape1.Name,
                args.Shape2.Name,
                args.Intersection);
        };
    }

    public string PlayButtonText
    {
        get
        {
            if (this.SelectedShape != null)
            {
                return this.SelectedShape.IsPaused
                    ? this.Localization.GetString("playButtonPlay")
                    : this.Localization.GetString("playButtonPause");
            }

            return this.Localization.GetString("playButtonSelect");
        }
    }

    public string CircleButtonText => this.Localization.GetString("circle");

    public string SquareButtonText => this.Localization.GetString("square");

    public string TriangleButtonText => this.Localization.GetString("triangle");

    public Point CanvasBoundary => new(this.CanvasWidth, this.CanvasHeight);

    public ObservableCollection<ShapeViewModel> Shapes { get; } = new();

    private Dictionary<ShapeViewModel, int> ShapeInvokeCountDictionary { get; } = new();

    private ILocalizationManager Localization { get; } = Ioc.Default.GetService<ILocalizationManager>()
                                                         ?? throw new ArgumentNullException(nameof(Localization));

    private IFileServiceFactory FileServiceFactory { get; } = Ioc.Default.GetService<IFileServiceFactory>()
                                                              ?? throw new ArgumentNullException(
                                                                  nameof(FileServiceFactory));

    private ILogger<MainViewModel> Logger { get; } = Ioc.Default.GetService<ILogger<MainViewModel>>()
                                                     ?? throw new ArgumentNullException(nameof(Logger));

    public event EventHandler<IntersectionEventArgs> IntersectionOccured;

    private void UpdateChildrenCanvasBoundary()
    {
        foreach (ShapeViewModel shape in this.Shapes)
        {
            shape.Boundary = this.CanvasBoundary;
        }
    }

    partial void OnCanvasHeightChanged(double value) => this.UpdateChildrenCanvasBoundary();

    partial void OnCanvasWidthChanged(double value) => this.UpdateChildrenCanvasBoundary();

    internal void MoveShapes()
    {
        foreach (ShapeViewModel shape in this.Shapes)
        {
            try
            {
                shape.Move();
            }
            catch (ShapeOutOfBoundsException e)
            {
                shape.JumpToBoundary();
                this.Logger.LogError(e, "{Message}", e.Message);
            }
        }

        foreach (ShapeViewModel shape in this.Shapes)
        {
            this.CheckIntersectionsWith(shape);
        }
    }

    private void CheckIntersectionsWith(ShapeViewModel shape)
    {
        ShapeViewModel[] contactShapes = this.Shapes.Where(
            x =>
                x.Kind == shape.Kind
                && Math.Abs(x.X - shape.X) < 5
                && Math.Abs(x.Y - shape.Y) < 5
                && x != shape).ToArray();

        // Simulate multiple event handler assignment
        for (int i = 0; i < this.ShapeInvokeCountDictionary[shape]; i++)
        {
            foreach (ShapeViewModel contact in contactShapes)
            {
                this.IntersectionOccured?.Invoke(
                    this,
                    new IntersectionEventArgs(
                        contact,
                        shape,
                        new Point(shape.X, shape.Y)));
            }
        }
    }

    [RelayCommand]
    private void CreateShape(SupportedShapes kind)
    {
        ShapeViewModel shape = new(
            kind,
            this.GetCountOf(kind),
            this.CanvasBoundary);
        this.AddShape(shape);
    }

    [RelayCommand]
    private void PlayOrPause(ShapeViewModel? shape)
    {
        if (shape != null)
        {
            shape.IsPaused = !shape.IsPaused;
        }

        // Dependent
        this.OnPropertyChanged(nameof(this.PlayButtonText));
    }

    [RelayCommand]
    private void ChangeCulture(CultureInfo cultureInfo) => this.Localization.UiCulture = cultureInfo;

    [RelayCommand]
    private async Task SaveTo(string path)
    {
        IEnumerable<ShapeDTO> shapeDtos = this.Shapes.Select(x => x.ToDTO());
        SupportedFileFormats format = ExtensionToSupportedConverter.Convert(Path.GetExtension(path));
        IFileService service = this.FileServiceFactory.Create(format, path);
        await service.SaveAsync(shapeDtos);
        this.Shapes.Clear();
    }

    [RelayCommand]
    private async Task LoadFrom(string path)
    {
        this.Shapes.Clear();
        SupportedFileFormats format = ExtensionToSupportedConverter.Convert(Path.GetExtension(path));
        ;
        IFileService service = this.FileServiceFactory.Create(format, path);
        ShapeDTO[] shapes = await service.LoadAsync();

        foreach (ShapeDTO shape in shapes)
        {
            this.AddShape(shape.ToViewModel(this.CanvasBoundary));
        }
    }

    internal void AddShape(ShapeViewModel shape)
    {
        shape.Boundary = this.CanvasBoundary;
        this.Shapes.Add(shape);
        this.ShapeInvokeCountDictionary.Add(shape, 0);
    }

    [RelayCommand]
    private void AddEventHandlerTo(ShapeViewModel? shape)
    {
        if (shape == null)
        {
            return;
        }

        this.ShapeInvokeCountDictionary[shape]++;
    }

    [RelayCommand]
    private void RemoveHandlerFrom(ShapeViewModel? shape)
    {
        if (shape == null)
        {
            return;
        }

        if (this.ShapeInvokeCountDictionary[shape] < 1)
        {
            return;
        }

        this.ShapeInvokeCountDictionary[shape]--;
    }

    private int GetCountOf(SupportedShapes kind) => this.Shapes.Count(x => x.Kind == kind);
}
