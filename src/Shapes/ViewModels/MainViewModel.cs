﻿// <copyright file="MainViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.Events;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.Wrappers;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public sealed partial class MainViewModel : ObservableRecipient
{
    public event EventHandler<IntersectionEventArgs> IntersectionOccured;

    private Dictionary<ShapeViewModel, int> ShapeInvokeCountDictionary { get; } = new();

    public Point CanvasBoundary => new(this.CanvasWidth, this.CanvasHeight);

    public string ButtonText
    {
        get
        {
            if (this.SelectedShape != null)
            {
                return this.SelectedShape.IsPaused
                    ? this.LocalizerService.PlayButtonPlay
                    : this.LocalizerService.PlayButtonPause;
            }

            return this.LocalizerService.PlayButtonSelect;
        }
    }

    public ObservableCollection<ShapeViewModel> Shapes { get; } = new();

    public LocalizerServiceObservableWrapper LocalizerService =>
        Ioc.Default.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(this.LocalizerService));

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasBoundary))]
    private double canvasHeight;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasBoundary))]
    private double canvasWidth;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ButtonText))]
    private ShapeViewModel? selectedShape;

    public MainViewModel()
    {
        this.FrameTimer = new DispatcherTimer(
            TimeSpan.FromMilliseconds(21),
            DispatcherPriority.Render,
            (_, _) => this.MoveShapes(),
            App.Current.Dispatcher);

        this.LocalizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.ButtonText));

        this.IntersectionOccured += (_, args) =>
        {
            Console.WriteLine($"{args.Shape1.Name} with {args.Shape2.Name} @ {args.Intersection}");
        };
    }

    private void UpdateChildrenCanvasBoundary()
    {
        foreach (var shape in this.Shapes)
        {
            shape.Boundary = this.CanvasBoundary;
        }
    }

    partial void OnCanvasHeightChanged(double value) => this.UpdateChildrenCanvasBoundary();

    partial void OnCanvasWidthChanged(double value) => this.UpdateChildrenCanvasBoundary();

    /// <summary>
    ///     Gets DispatcherTimer with frame time interval
    /// </summary>
    private DispatcherTimer FrameTimer { get; }

    internal void MoveShapes()
    {
        foreach (var shape in this.Shapes)
        {
            shape.Move();
            this.CheckIntersectionsWith(shape);
        }
    }

    private void CheckIntersectionsWith(ShapeViewModel shape)
    {
        var contactShapes = this.Shapes.Where(
            x =>
                x.Kind == shape.Kind
                && Math.Abs(x.X - shape.X) < 1
                && Math.Abs(x.Y - shape.Y) < 1
                && x != shape).ToArray();

        // Simulate multiple event handler assignment
        for (var i = 0; i < this.ShapeInvokeCountDictionary[shape]; i++)
        {
            foreach (var contact in contactShapes)
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
        var shape = new ShapeViewModel(
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
        this.OnPropertyChanged(nameof(this.ButtonText));
    }

    [RelayCommand]
    private void ChangeCulture(CultureInfo cultureInfo)
    {
        this.LocalizerService.CurrentCulture = cultureInfo;
    }

    [RelayCommand]
    private void SaveTo(IFileService service)
    {
        service.Save(this.Shapes);
        this.Shapes.Clear();
    }

    [RelayCommand]
    private void LoadFrom(IFileService service)
    {
        this.Shapes.Clear();
        var shapes = service.Load().ToArray();

        foreach (var shape in shapes)
        {
            this.AddShape(shape);
        }

        this.UpdateChildrenCanvasBoundary();
    }

    internal void AddShape(ShapeViewModel shape)
    {
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

    private int GetCountOf(SupportedShapes kind)
    {
        return this.Shapes.Count(x => x.Kind == kind);
    }
}