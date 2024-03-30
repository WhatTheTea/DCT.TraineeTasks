// <copyright file="ShapeDTO.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.Common;

public record ShapeDTO(
    int Id,
    double X,
    double Y,
    bool IsPaused,
    SupportedShapes Kind,
    (double x, double y) Velocity)
{
    private ShapeDTO()
        : this(0, 0, 0, false, 0, (0, 0))
    {
    }
}
