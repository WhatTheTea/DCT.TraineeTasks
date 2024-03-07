// <copyright file="MovingCircle.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Views.MovingShapes;
using System.Windows;
using System.Windows.Media;

public class MovingCircle : MovingShape
{
    private static int CircleCount;

    public override string ToString() => "Circle " + this.Id;

    /// <summary>
    /// Initializes a new instance of the <see cref="MovingCircle"/> class.
    /// </summary>
    /// <param name="boundary">The most down-left point.</param>
    public MovingCircle(Point boundary)
        : base(boundary)
    {
        this.DefiningGeometry = new EllipseGeometry(new Rect(0, 0, 30, 30));
        CircleCount++;
        this.Stroke = new SolidColorBrush(Colors.Lime);
        this.Height = 30;
        this.Width = 30;
        this.OffsetX = 7;
        this.OffsetY = 7;
        this.Id = CircleCount;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="MovingCircle"/> class.
    /// </summary>
    ~MovingCircle() => CircleCount--;

    /// <inheritdoc/>
    protected override Geometry DefiningGeometry { get; }
}