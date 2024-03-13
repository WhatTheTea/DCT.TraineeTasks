﻿// <copyright file="TriangleViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Models.MovingShapes;
using DCT.TraineeTasks.Shapes.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DCT.TraineeTasks.Shapes.ViewModels.Shapes;

public class TriangleViewModel : ShapeViewModel
{
    public TriangleViewModel(MovingShape shape)
        : base(shape)
    {
        this.WhenAnyValue(x => localizedStrings.Triangle)
            .ToPropertyEx(this, x => x.Name);
    }
}