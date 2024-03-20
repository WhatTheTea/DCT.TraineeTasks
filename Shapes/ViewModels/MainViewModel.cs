// <copyright file="MainViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Shapes.Primitives;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private double canvasHeight;

    [ObservableProperty] private double canvasWidth;

    private ShapeViewModel? selectedShape;

    public MainViewModel()
    {
        this.FrameTimer.Start();

        this.FrameTimer.Tick += (_, _) =>
        {
            foreach (var shape in this.Shapes)
            {
                shape.Boundary = new Point(this.canvasWidth, this.canvasHeight);
                shape.Move();
            }
        };

        this.LocalizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.ButtonText));
    }

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

    public ShapeViewModel? SelectedShape
    {
        get => this.selectedShape;
        set
        {
            this.SetProperty(ref this.selectedShape, value);

            // Dependent:
            this.OnPropertyChanged(nameof(this.ButtonText));
        }
    }

    public ObservableCollection<ShapeViewModel> Shapes { get; } = new();

    public LocalizerServiceObservableWrapper LocalizerService =>
        App.Current.Services.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(this.LocalizerService));

    public IFileService BinFileService =>
        App.Current.Services.GetService<BinaryFileService>()
        ?? throw new ArgumentNullException(nameof(this.BinFileService));
    
    public IFileService JsonFileService =>
        App.Current.Services.GetService<JsonFileService>()
        ?? throw new ArgumentNullException(nameof(this.JsonFileService));

    /// <summary>
    ///     Gets DispatcherTimer with frame time interval
    /// </summary>
    private DispatcherTimer FrameTimer { get; } = new()
    {
        Interval = TimeSpan.FromMilliseconds(21),
    };

    [RelayCommand]
    private void AddShape(SupportedShapes kind)
    {
        var shape = new ShapeViewModel(
            kind,
            this.GetCountOf(kind),
            new Point(this.CanvasWidth, this.CanvasHeight));
        this.Shapes.Add(shape);
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
    private void SaveTo(SupportedFileFormats format)
    {
        // TODO: Can be refactored to converter
        switch (format) 
        {
            case SupportedFileFormats.Bin:
                this.BinFileService.Save(this.Shapes);
                break;
            case SupportedFileFormats.JSON:
                this.JsonFileService.Save(this.Shapes);
                break;
            case SupportedFileFormats.Xml:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }

        this.Shapes.Clear();
    }

    [RelayCommand]
    private void LoadFrom(SupportedFileFormats format)
    {
        this.Shapes.Clear();
        var shapes = Array.Empty<ShapeViewModel>();
        switch (format)
        {
            case SupportedFileFormats.Bin:
                shapes = this.BinFileService.Load().ToArray();
                break;
            case SupportedFileFormats.JSON:
                shapes = this.JsonFileService.Load().ToArray();
                break;
            case SupportedFileFormats.Xml:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }

        foreach (var shape in shapes)
        {
            shape.Boundary = new Point(this.CanvasWidth, this.CanvasHeight);
            this.Shapes.Add(shape);
        }
    }

    private int GetCountOf(SupportedShapes kind)
    {
        return this.Shapes.Count(x => x.ShapeKind == kind);
    }
}