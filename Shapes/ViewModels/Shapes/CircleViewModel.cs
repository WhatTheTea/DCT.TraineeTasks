﻿// <copyright file="CircleViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reactive.Linq;
using DCT.TraineeTasks.Shapes.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DCT.TraineeTasks.Shapes.ViewModels.Shapes;

public class CircleViewModel : ShapeViewModel
{
    public CircleViewModel()
        : base()
    {
        this.Name = this.LocalizedStrings.Circle;
        this.WhenAnyValue(x => x.LocalizedStrings.Circle)
            .BindTo(this, x => x.Name);
    }
}