using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Randomizer;

public class PointRandomizer
{
    public static Point Next(Point upperBoundary)
    {
        return new Point(
            Random.Shared.Next(0, (int)upperBoundary.X),
            Random.Shared.Next(0, (int)upperBoundary.Y));
    }
}