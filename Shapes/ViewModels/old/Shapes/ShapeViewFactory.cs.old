// <copyright file="ShapeViewModelFactory.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows.Media;
using DCT.TraineeTasks.Shapes.Views;
using Splat;

namespace DCT.TraineeTasks.Shapes.ViewModels.Shapes;

public class ShapeViewFactory
{
    public MovingShape MakeCircle()
    {
        var vm = Locator.Current.GetService<CircleViewModel>();
        var circle = new MovingShape(MovingShapesGeometries.Circle, vm)
        {
            Stroke = Brushes.Blue,
            Width = 20,
            Height = 20,
        };
        return circle;
    }

    public MovingShape MakeTriangle()
    {
        var vm = Locator.Current.GetService<TriangleViewModel>();
        var triangle = new MovingShape(MovingShapesGeometries.Triangle, vm)
        {
            Stroke = Brushes.DarkGoldenrod,
            Width = 20,
            Height = 20,
        };
        return triangle;
    }

    public MovingShape MakeSquare()
    {
        var vm = Locator.Current.GetService<SquareViewModel>();
        var square = new MovingShape(MovingShapesGeometries.Rectangle, vm)
        {
            Stroke = Brushes.Indigo,
            Width = 20,
            Height = 20,
        };
        return square;
    }
}