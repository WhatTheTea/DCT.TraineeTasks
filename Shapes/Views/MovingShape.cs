// <copyright file="MovingShape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows.Media;
using System.Windows.Shapes;

namespace DCT.TraineeTasks.Shapes.Views;

public class MovingShape : Shape
{
    protected override Geometry DefiningGeometry { get; }

    public double OffsetX { get; set; }
    public double OffsetY { get; set; }
}