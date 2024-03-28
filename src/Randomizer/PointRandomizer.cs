﻿using DCT.TraineeTasks.Primitives;

namespace DCT.TraineeTasks.Randomizer;

public class PointRandomizer
{
    public static Point Next(Point upperBoundary)
    {
        return new Point(
            Random.Shared.Next(0, (int)upperBoundary.X),
            Random.Shared.Next(0, (int)upperBoundary.Y));
    }
}