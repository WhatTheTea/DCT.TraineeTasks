// <copyright file="MovingShape.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DCT.TraineeTasks.Shapes.ViewModels.Shapes;
using ReactiveUI;

namespace DCT.TraineeTasks.Shapes.Views;

public class MovingShape : Shape, IViewFor<ShapeViewModel>
{
    public MovingShape(Geometry definingGeometry)
    {
        this.DefiningGeometry = definingGeometry;

        this.WhenActivated(
            d =>
            {
                this.OneWayBind(
                    this.ViewModel,
                    vm => vm.Position,
                    v => v.RenderTransform,
                    x => new TranslateTransform(x.X, x.Y))
                    .DisposeWith(d);
            });
    }

    public Guid Id { get; } = Guid.NewGuid();

    public void MoveTo(Point point) =>
        this.RenderTransform = new TranslateTransform(point.X, point.Y);

    protected override Geometry DefiningGeometry { get; }

    object? IViewFor.ViewModel
    {
        get => this.ViewModel;
        set => this.ViewModel = (ShapeViewModel?)value;
    }

    public ShapeViewModel? ViewModel { get; set; }
}