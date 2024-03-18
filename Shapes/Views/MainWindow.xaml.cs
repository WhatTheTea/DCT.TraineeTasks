// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
    }

    public MainViewModel ViewModel => (MainViewModel)this.DataContext;

    private Point Boundary
    {
        get => new(this.ShapesCanvasItemsControl.ActualWidth, this.ShapesCanvasItemsControl.ActualHeight);
        set => _ = value;
    }
}