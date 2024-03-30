using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Randomizer;

public static class PointRandomizer
{
    public static Point Next(Point upperBoundary) =>
        new(
            Random.Shared.Next(0, (int)upperBoundary.X),
            Random.Shared.Next(0, (int)upperBoundary.Y));
}
