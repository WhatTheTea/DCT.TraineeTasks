// <copyright file = "Point.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Common;

public sealed record Point(double X = 0, double Y = 0)
{
    public Point((double x, double y) tuple)
        : this(tuple.x, tuple.y)
    {
    }

    public double X { get; set; } = X;

    public double Y { get; set; } = Y;

    public void Deconstruct(out double x, out double y)
    {
        x = this.X;
        y = this.Y;
    }
}
