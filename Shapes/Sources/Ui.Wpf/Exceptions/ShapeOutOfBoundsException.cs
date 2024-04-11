// <copyright file = "ShapeOutOfBoundsException.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Exceptions;

public class ShapeOutOfBoundsException(ShapeViewModel shape)
    : InvalidOperationException($"{shape.Name} is out of bounds: {new Point()} : {shape.Boundary}!");
