// <copyright file="MainViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
                shape.UpdateBoundary(this.canvasWidth, this.canvasHeight);
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
            (this.CanvasWidth, this.CanvasHeight));
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
    private void SaveTo(SupportedFileFormats formats)
    {
        switch (formats)
        {
            case SupportedFileFormats.Bin:
                this.BinFileService.Save(this.Shapes);
                break;
            case SupportedFileFormats.JSON:
                break;
            case SupportedFileFormats.Xml:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(formats), formats, null);
        }
    }

    private int GetCountOf(SupportedShapes kind) =>
        this.Shapes.Count(x => x.ShapeKind == kind);
}