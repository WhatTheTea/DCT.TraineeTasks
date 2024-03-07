// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MovingShapes;
using ViewModels;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    private List<MovingShape> movingShapes = new();

    private Point Boundary => new (this.ShapesCanvas.ActualWidth, this.ShapesCanvas.ActualHeight); 

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        this.ViewModel = new MainWindowViewModel();
        var timer = new DispatcherTimer();
        timer.Tick += (_, _) =>
        {
            foreach (var shape in this.movingShapes)
            {
                shape.Boundary = this.Boundary;
                shape.Move();
            }
        };
        timer.Interval = TimeSpan.FromMilliseconds(21);
        timer.Start();
    }

    private void SquareButton_OnClick(object sender, RoutedEventArgs e)
    {
        var shape = new MovingRectangle(this.Boundary);
        this.AddShape(shape);
    }

    private void TriangleButton_OnClick(object sender, RoutedEventArgs e)
    {
        var shape = new MovingTriangle(this.Boundary);
        this.AddShape(shape);
    }

    private void CircleButton_OnClick(object sender, RoutedEventArgs e)
    {
        var shape = new MovingCircle(this.Boundary);
        this.AddShape(shape);
    }

    private void AddShape(MovingShape shape)
    {
        this.movingShapes.Add(shape);
        this.ShapesListBox.Items.Add(shape.ToString());
        this.ShapesCanvas.Children.Add(shape);
        Canvas.SetTop(shape, 10);
        Canvas.SetLeft(shape, 10);
    }

}