// <copyright file="MovingShapesGeometries.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Media;

namespace DCT.TraineeTasks.Shapes.Views;

public static class MovingShapesGeometries
{
    public static Geometry Circle => new EllipseGeometry(new Rect(0, 0, 30, 30));

    public static Geometry Rectangle => new RectangleGeometry
    {
        Rect = new Rect(0, 0, 20, 20),
    };

    public static Geometry Triangle => new PathGeometry(
    [
        new PathFigure(
            new Point(20, 20),
            new List<PathSegment>
            {
                new LineSegment(new Point(40, 0), true),
                new LineSegment(new Point(0, 0), true),
            }.AsReadOnly(),
            true)
    ]);
}