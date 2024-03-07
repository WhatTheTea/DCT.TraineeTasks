// <copyright file="MovingShape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public abstract class MovingShape : Shape
{
    public double OffsetX { get; set; }

    public double OffsetY { get; set; }

    protected Point boundary;

    protected MovingShape(Point boundary)
    {
        this.boundary = boundary;
    }

    public void Move()
    {
        var transform = this.RenderTransform.Transform(default);
        this.CheckOffsets(transform);
        this.RenderTransform = new TranslateTransform(transform.X + this.OffsetX, transform.Y + this.OffsetY);
    }

    private void CheckOffsets(Point transform)
    {
        var nextPoint = new Point(transform.X + this.OffsetX, transform.Y + this.OffsetY);
        if (nextPoint.X >= this.boundary.X || nextPoint.X <= 0)
        {
            this.OffsetX *= -1;
        }

        if (nextPoint.Y >= this.boundary.Y || nextPoint.Y <= 0)
        {
            this.OffsetY *= -1;
        }
    }
}