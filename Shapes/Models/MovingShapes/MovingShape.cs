// <copyright file="MovingShape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Splat;

namespace DCT.TraineeTasks.Shapes.Models.MovingShapes;

public abstract class MovingShape : Shape
{
    public Guid Id { get; } = Guid.NewGuid();

    public void MoveTo(Point point)
    {
        var transform = this.RenderTransform.Transform(default);
        this.RenderTransform = new TranslateTransform(point.X, point.Y);
    }
}