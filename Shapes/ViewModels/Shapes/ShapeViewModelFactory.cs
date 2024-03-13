// <copyright file="ShapeViewModelFactory.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows.Media;
using DCT.TraineeTasks.Shapes.Views;

namespace DCT.TraineeTasks.Shapes.ViewModels.Shapes;

public class ShapeViewModelFactory
{
    public CircleViewModel MakeCircle()
    {
        var shape = new MovingShape(MovingShapesGeometries.Circle)
        {
            Stroke = Brushes.Blue,
            Width = 20,
            Height = 20,
        };
        var circle = new CircleViewModel(shape);
        return circle;
    }

    public TriangleViewModel MakeTriangle()
    {
        var shape = new MovingShape(MovingShapesGeometries.Triangle)
        {
            Stroke = Brushes.DarkGoldenrod,
            Width = 20,
            Height = 20,
        };
        var triangle = new TriangleViewModel(shape);
        return triangle;
    }

    public SquareViewModel MakeSquare()
    {
        var shape = new MovingShape(MovingShapesGeometries.Rectangle)
        {
            Stroke = Brushes.Indigo,
            Width = 20,
            Height = 20,
        };
        var square = new SquareViewModel(shape);
        return square;
    }
}