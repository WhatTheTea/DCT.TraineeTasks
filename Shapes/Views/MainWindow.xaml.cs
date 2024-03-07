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

    private MovingShape? SelectedShape
    {
        get
        {
            var selected = this.ShapesListBox.SelectedItem as string ?? string.Empty;
            var found = this.movingShapes.FirstOrDefault(x => x.ToString() == selected);
            return found;
        }
    }

    private Point Boundary => new(this.ShapesCanvas.ActualWidth, this.ShapesCanvas.ActualHeight);

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

    private void AddShape(MovingShape shape)
    {
        this.movingShapes.Add(shape);
        this.ShapesListBox.Items.Add(shape.ToString());
        this.ShapesCanvas.Children.Add(shape);
        Canvas.SetTop(shape, 10);
        Canvas.SetLeft(shape, 10);
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

    private void PlayButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (this.SelectedShape == null)
        {
            return;
        }

        if (this.SelectedShape.isPaused)
        {
            this.SelectedShape.UnPause();
        }
        else
        {
            this.SelectedShape.Pause();
        }

        this.PlayButton.Content = this.SelectedShape.isPaused ? "Play" : "Pause";
    }

    private void ShapesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (this.SelectedShape != null)
        {
            this.PlayButton.Content = this.SelectedShape.isPaused ? "Play" : "Pause";
        }
    }
}