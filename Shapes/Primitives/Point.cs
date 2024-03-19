// <copyright file="Point.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Primitives;

public record Point(double x = 0, double y = 0)
{
    public double X { get; } = x;

    public double Y { get; } = y;

    public void Deconstruct(out double x, out double y)
    {
        x = this.X;
        y = this.Y;
    }
}