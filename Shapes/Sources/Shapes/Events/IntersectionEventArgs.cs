// <copyright file="IntersectionEventArgs.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Events;

public class IntersectionEventArgs(ShapeViewModel shape1, ShapeViewModel shape2, Point intersection)
    : EventArgs
{
    public ShapeViewModel Shape1 => shape1;

    public ShapeViewModel Shape2 => shape2;

    public Point Intersection => intersection;
}