// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using DCT.TraineeTasks.Shapes.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace DCT.TraineeTasks.Shapes.ViewModels.Shapes;

public abstract class ShapeViewModel : ReactiveObject
{
    protected LocalizerService LocalizedStrings = Locator.Current.GetService<LocalizerService>()
        ?? throw new ArgumentNullException(nameof(LocalizedStrings));

    // public MovingShape Shape { get; }

    [ObservableAsProperty]
    public bool IsPaused { get; }

    [ObservableAsProperty]
    public string Name { get; }

    [Reactive] 
    public Vector Velocity { get; set; }

    [Reactive]
    public Point Position { get; set; }

    [Reactive]
    public Point Boundary { get; set; }
    
    [Reactive]
    public ReactiveCommand<Unit, Unit> Move { get; private set; }
    
    public ReactiveCommand<Unit, Unit> PlayPause { get; }

    protected ReactiveCommand<Unit, Unit> MoveActive;

    protected ReactiveCommand<Unit, Unit> MovePaused;

    protected ShapeViewModel(MovingShape shape)
    {
        // this.Shape = shape;

        this.MoveActive = ReactiveCommand
            .Create(
                () =>
                {
                    var nextPosition = this.NextPosition;
                    this.BoundaryBump();
                    // this.Shape.MoveTo(nextPosition);
                    this.Position = nextPosition;
                });

        this.MovePaused = ReactiveCommand.Create(() => { });

        this.Move = this.MoveActive;

        this.PlayPause = ReactiveCommand.Create(
            () =>
            {
                this.Move = this.IsPaused ? this.MoveActive : this.MovePaused;
            });

        this.WhenAnyValue(x => x.Move)
            .Select(x => x == this.MovePaused)
            .ToPropertyEx(this, x => x.IsPaused);
    }

    private Point NextPosition => new(
        this.Position.X + this.Velocity.X,
        this.Position.Y + this.Velocity.Y);

    private void BoundaryBump()
    {
        var velocityX = this.Velocity.X;
        var velocityY = this.Velocity.Y;
        var nextPoint = this.NextPosition;
        if (nextPoint.X >= this.Boundary.X || nextPoint.X <= 0)
        {
            velocityX *= -1;
        }

        if (nextPoint.Y >= this.Boundary.Y || nextPoint.Y <= 0)
        {
            velocityY *= -1;
        }

        this.Velocity = new Vector { X = velocityX, Y = velocityY };
    }
}