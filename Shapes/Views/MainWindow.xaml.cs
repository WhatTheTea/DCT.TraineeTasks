// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reactive.Disposables;
using System.Windows.Controls.Primitives;
using ReactiveUI;
using Splat;

namespace DCT.TraineeTasks.Shapes.Views;

using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Threading;
using ViewModels;
using MovingShapes;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    public DispatcherTimer Timer { get; set; }

    private Point Boundary
    {
        get => new(this.ShapesCanvasItemsControl.ActualWidth, this.ShapesCanvasItemsControl.ActualHeight);
        set => _ = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        this.ViewModel = Locator.Current.GetService<MainWindowViewModel>();
        this.Timer = new DispatcherTimer();
        this.Timer.Interval = TimeSpan.FromMilliseconds(21);
        this.WhenActivated(
            d =>
            {
                this.ViewModel.Boundary = Boundary;
                this.OneWayBind(
                    this.ViewModel,
                    vm => vm.MovingShapesNames,
                    v => v.ShapesListBox.ItemsSource)
                    .DisposeWith(d);
                this.OneWayBind(
                    this.ViewModel,
                    vm => vm.MovingShapes,
                    v => v.ShapesCanvasItemsControl.ItemsSource)
                    .DisposeWith(d);
                this.Bind(
                    this.ViewModel,
                    vm => vm.Boundary,
                    v => v.Boundary,
                    this.ShapesCanvasItemsControl.Events().SizeChanged)
                    .DisposeWith(d);
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.AddCircle,
                        v => v.CircleButton)
                    .DisposeWith(d);
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.TimerTick,
                        v => v.Timer,
                        nameof(Timer.Tick))
                    .DisposeWith(d);
            });
        this.Timer.Start();
    }

    // private void PlayButton_OnClick(object sender, RoutedEventArgs e)
    // {
    //     if (this.SelectedShape == null)
    //     {
    //         return;
    //     }
    //
    //     if (this.SelectedShape.IsPaused)
    //     {
    //         this.SelectedShape.UnPause();
    //     }
    //     else
    //     {
    //         this.SelectedShape.Pause();
    //     }
    //
    //     this.PlayButton.Content = this.SelectedShape.IsPaused ? "Play" : "Pause";
    // }
    //
    // private void ShapesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    // {
    //     if (this.SelectedShape != null)
    //     {
    //         this.PlayButton.Content = this.SelectedShape.IsPaused ? "Play" : "Pause";
    //     }
    // }
    private void PlayButton_OnClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine(this.ShapesListBox.Items[0]);
    }
}