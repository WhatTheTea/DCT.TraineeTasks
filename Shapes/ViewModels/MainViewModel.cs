// <copyright file="MainViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
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
                shape.Move();
            }
        };

        this.LocalizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.ButtonText));
    }

    public ObservableCollection<ShapeViewModel> Shapes { get; } = new();

    public LocalizerServiceObservableWrapper LocalizerService =>
        App.Current.Services.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(this.LocalizerService));

    /// <summary>
    ///     Gets DispatcherTimer with frame time interval
    /// </summary>
    private DispatcherTimer FrameTimer { get; } = new()
    {
        Interval = TimeSpan.FromMilliseconds(21)
    };


    [RelayCommand]
    private void AddShape(SupportedShapes kind)
    {
        var shape = new ShapeViewModel(1337, "kek", kind);
        this.Shapes.Add(shape);
    }

    [RelayCommand]
    private void PlayOrPause(ShapeViewModel? shape)
    {
        shape?.PauseToggleCommand.Execute(null);

        // Dependent
        this.OnPropertyChanged(nameof(this.ButtonText));
    }

    [RelayCommand]
    private void ChangeCulture(CultureInfo cultureInfo)
    {
        this.LocalizerService.CurrentCulture = cultureInfo;
    }

    [RelayCommand]
    private void SaveTo(FileFormat format)
    {
        // TODO
    }
}