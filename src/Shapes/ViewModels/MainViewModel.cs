// <copyright file="MainViewModel.cs" company="Digital Cloud Technologies">
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

public sealed partial class MainViewModel : ObservableObject
{
    public event EventHandler<IntersectionEventArgs> IntersectionOccured;
    
    [ObservableProperty] private double canvasHeight;

    [ObservableProperty] private double canvasWidth;

    private ShapeViewModel? selectedShape;

    public MainViewModel()
    {
        this.FrameTimer.Start();

        this.FrameTimer.Tick += (_, _) => this.MoveShapes();

        this.LocalizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.ButtonText));
    }

    private void MoveShapes()
    {
        var pointsDictionary = new Dictionary<Point, ShapeViewModel>();
        foreach (var shape in this.Shapes)
        {
            shape.Boundary = new Point(this.canvasWidth, this.canvasHeight);
            shape.Move();
            var shapePosition = new Point(shape.X, shape.Y);

            if (pointsDictionary.TryAdd(shapePosition, shape))
            {
                continue;
            }

            // TryAdd fails, so current shape position is key for first shape with this position
            var firstShape = pointsDictionary[shapePosition];
            this.IntersectionOccured?.Invoke(
                this,
                new IntersectionEventArgs(
                    firstShape,
                    shape,
                    shapePosition));
        }
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
        Ioc.Default.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(this.LocalizerService));

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
            shape.Boundary = new Point(this.CanvasWidth, this.CanvasHeight);
            this.Shapes.Add(shape);
        }
    }

    private int GetCountOf(SupportedShapes kind)
    {
        return this.Shapes.Count(x => x.ShapeKind == kind);
    }
}