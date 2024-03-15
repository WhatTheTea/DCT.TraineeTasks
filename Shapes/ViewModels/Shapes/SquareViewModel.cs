// <copyright file="SquareViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Models.MovingShapes;
using DCT.TraineeTasks.Shapes.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DCT.TraineeTasks.Shapes.ViewModels.Shapes;

public class SquareViewModel : ShapeViewModel
{
    public SquareViewModel()
        : base()
    {
        this.Name = this.LocalizedStrings.Square;
        this.WhenAnyValue(x => x.LocalizedStrings.Square)
            .BindTo(this, x => x.Name);
    }
}