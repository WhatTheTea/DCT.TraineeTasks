// <copyright file="ShapeOutOfBoundsException.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Exceptions;

public class ShapeOutOfBoundsException(ShapeViewModel shape)
    : InvalidOperationException($"{shape.Name} is out of bounds: {new Point()} : {shape.Boundary}!");