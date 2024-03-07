// <copyright file="MovingTriangle.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Media;

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;

public class MovingTriangle : MovingShape
{
    public MovingTriangle(Point boundary) : base(boundary)
    {
        this.DefiningGeometry = new PathGeometry(
        [
            new PathFigure(
                new Point(20, 20),
                new List<PathSegment>
                {
                    new LineSegment(new(40, 0), true),
                    new LineSegment(new(0, 0), true),
                }.AsReadOnly(),
                true),
        ]);
    }

    protected override Geometry DefiningGeometry { get; }
}