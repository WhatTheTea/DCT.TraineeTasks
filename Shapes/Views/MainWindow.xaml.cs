// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Threading;
using DCT.TraineeTasks.Shapes.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    public MainViewModel ViewModel => (MainViewModel)this.DataContext;

    /// <summary>
    ///     Gets DispatcherTimer with frame time interval
    /// </summary>
    private DispatcherTimer FrameTimer { get; } = new()
    {
        Interval = TimeSpan.FromMilliseconds(21),
    };

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        // this.DataContext = App.Current.Services.GetService<MainViewModel>();
        this.FrameTimer.Start();
    }

    private Point Boundary
    {
        get => new(this.ShapesCanvasItemsControl.ActualWidth, this.ShapesCanvasItemsControl.ActualHeight);
        set => _ = value;
    }
}