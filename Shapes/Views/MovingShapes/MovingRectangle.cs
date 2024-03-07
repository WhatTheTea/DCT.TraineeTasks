// <copyright file="MovingRectangle.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;

using System.Windows;
using System.Windows.Media;

public class MovingRectangle : MovingShape
{
    private static int RectangleCount;

    public override string ToString() => "Square " + this.Id;
    

    public MovingRectangle(Point boundaries)
        : base(boundaries)
    {
        this.DefiningGeometry = new RectangleGeometry
        {
            Rect = new Rect(0, 0, 20, 20),
        };
        RectangleCount++;
        this.Stroke = new SolidColorBrush(Colors.RoyalBlue);
        this.Height = 40;
        this.Width = 40;
        this.OffsetX = 6;
        this.OffsetY = 6;
        this.Id = RectangleCount;
    }

    ~MovingRectangle() => RectangleCount--;

    /// <inheritdoc/>
    protected override Geometry DefiningGeometry { get; }
}