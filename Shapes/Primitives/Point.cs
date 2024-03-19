// <copyright file="Point.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Primitives;

public record Point(double X = 0, double Y = 0)
{
    public Point((double x, double y) tuple)
        : this() =>
        (X, Y) = tuple;
    public void Deconstruct(out double x, out double y)
    {
        x = this.X;
        y = this.Y;
    }
}