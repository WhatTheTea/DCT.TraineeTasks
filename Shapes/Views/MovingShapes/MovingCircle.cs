// <copyright file="MovingCircle.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;
using System.Windows;
using System.Windows.Media;

public class MovingCircle : MovingShape
{
    public MovingCircle(Point boundaries)
        : base(boundaries)
    {
        this.DefiningGeometry = new EllipseGeometry(new Rect(0, 0, 30, 30));

        this.Stroke = new SolidColorBrush(Colors.Lime);
        this.Height = 30;
        this.Width = 30;
        this.OffsetX = 7;
        this.OffsetY = 7;
    }

    /// <inheritdoc/>
    protected override Geometry DefiningGeometry { get; }
}