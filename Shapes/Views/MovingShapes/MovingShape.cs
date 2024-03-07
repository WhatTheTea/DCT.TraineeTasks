// <copyright file="MovingShape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public abstract class MovingShape : Shape
{
    public Point Boundary { get; set; }

    protected MovingShape(Point boundary)
    {
        this.Boundary = boundary;
        var random = new Random();
        this.RenderTransform = new TranslateTransform(random.Next(0, 300), random.Next(0, 300));
    }

    protected double OffsetX { get; set; }

    protected double OffsetY { get; set; }

    public void Move()
    {
        var transform = this.RenderTransform.Transform(default);
        this.CheckOffsets(transform);
        this.RenderTransform = new TranslateTransform(transform.X + this.OffsetX, transform.Y + this.OffsetY);
    }

    private void CheckOffsets(Point transform)
    {
        var nextPoint = new Point(transform.X + this.OffsetX, transform.Y + this.OffsetY);
        if (nextPoint.X >= this.Boundary.X || nextPoint.X <= 0)
        {
            this.OffsetX *= -1;
        }

        if (nextPoint.Y >= this.Boundary.Y || nextPoint.Y <= 0)
        {
            this.OffsetY *= -1;
        }
    }
}