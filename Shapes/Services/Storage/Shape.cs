// <copyright file="Shape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Resources;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public record ShapeDTO(
    double x,
    double y,
    bool isPaused,
    SupportedShapes kind,
    int id);