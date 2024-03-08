﻿// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
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
                this.BindCommands(d);
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
                this.WhenAnyValue(x => x.ShapesListBox.SelectedItem)
                    .BindTo(this.ViewModel, x => x.SelectedShapeName)
                    .DisposeWith(d);
                this.OneWayBind(
                    this.ViewModel,
                    vm => vm.PlayButtonText,
                    v => v.PlayButton.Content)
                    .DisposeWith(d);
            });
        this.Timer.Start();
    }

    private void BindCommands(CompositeDisposable d)
    {
        this.BindShapesButtons(d);
        this.BindCommand(
                this.ViewModel,
                vm => vm.MoveShapes,
                v => v.Timer,
                nameof(this.Timer.Tick))
            .DisposeWith(d);
        this.BindCommand(
                this.ViewModel,
                vm => vm.PlayPause,
                v => v.PlayButton)
            .DisposeWith(d);
    }

    private void BindShapesButtons(CompositeDisposable d)
    {
        this.BindCommand(
                this.ViewModel,
                vm => vm.AddCircle,
                v => v.CircleButton)
            .DisposeWith(d);
        this.BindCommand(
                this.ViewModel,
                vm => vm.AddSquare,
                v => v.SquareButton)
            .DisposeWith(d);
        this.BindCommand(
                this.ViewModel,
                vm => vm.AddTriangle,
                v => v.TriangleButton)
            .DisposeWith(d);
    }
}