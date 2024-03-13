// <copyright file="MovingShape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DCT.TraineeTasks.Shapes.Views;

public class MovingShape : Shape
{
    public MovingShape(Geometry definingGeometry)
    {
        this.DefiningGeometry = definingGeometry;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public void MoveTo(Point point)
    {
        var transform = this.RenderTransform.Transform(default);
        this.RenderTransform = new TranslateTransform(point.X, point.Y);
    }

    protected override Geometry DefiningGeometry { get; }
}