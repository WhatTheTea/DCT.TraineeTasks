// <copyright file="MovingTriangle.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Media;

namespace DCT.TraineeTasks.Shapes.MovingShapes;

public class MovingTriangle : MovingShape
{
    private static int TriangleCount;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MovingTriangle" /> class.
    /// </summary>
    /// <param name="boundary">Max XY position</param>
    public MovingTriangle(Point boundary)
        : base(boundary)
    {
        this.DefiningGeometry = new PathGeometry(
        [
            new PathFigure(
                new Point(20, 20),
                new List<PathSegment>
                {
                    new LineSegment(new Point(40, 0), true),
                    new LineSegment(new Point(0, 0), true)
                }.AsReadOnly(),
                true)
        ]);
        TriangleCount++;
        this.Stroke = new SolidColorBrush(Colors.Gold);
        this.Height = 50;
        this.Width = 50;
        this.OffsetX = 9;
        this.OffsetY = 9;
        this.Id = TriangleCount;
    }

    /// <inheritdoc />
    protected override Geometry DefiningGeometry { get; }

    public override string ToString()
    {
        return this.LocalizedStrings.Triangle + this.Id;
    }
}