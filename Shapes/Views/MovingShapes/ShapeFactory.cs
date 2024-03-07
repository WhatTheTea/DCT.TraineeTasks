// <copyright file="ShapeFactory.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;

using System.Windows.Media;

public class ShapeFactory
{
    public MovingShape MakeRectangle(Point boundary)
    {
        var rectangle = new MovingRectangle(new Rect(10, 10, 20, 20), boundary)
        {
            Fill = new SolidColorBrush(Colors.RoyalBlue),
            Height = 40,
            Width = 40,
            OffsetX = 6,
            OffsetY = 6,
        };
        return rectangle;
    }

    public MovingShape MakeTriangle(Point boundary)
    {
        var triangle = new MovingTriangle(boundary)
        {
            Fill = new SolidColorBrush(Colors.Gold),
            Height = 50,
            Width = 50,
            OffsetX = 9,
            OffsetY = 9,
        };
        return triangle;
    }
    //
    // public MovingShape MakeCircle()
    // {
    //     
    // }
}