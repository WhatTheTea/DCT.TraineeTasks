// <copyright file="MovingRectangle.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;

using System.Windows;
using System.Windows.Media;

public class MovingRectangle : MovingShape
{
    public MovingRectangle(Rect rectangle, Point boundaries)
        : base(boundaries)
    {
        this.DefiningGeometry = new RectangleGeometry
        {
            Rect = rectangle,
        };
    }

    /// <inheritdoc/>
    protected override Geometry DefiningGeometry { get; }
}