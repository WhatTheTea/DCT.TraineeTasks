// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reflection.Metadata;
using System.Windows.Media.Animation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Randomizer;
using DCT.TraineeTasks.Shapes.Converters;
using DCT.TraineeTasks.Shapes.Exceptions;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Wrappers;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public partial class ShapeViewModel : ObservableObject
{
    internal LocalizerServiceObservableWrapper LocalizerService { get; } =
        Ioc.Default.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(LocalizerService));

    [ObservableProperty]
    private bool isPaused;
    
    private double x;
    private double y;

    public ShapeViewModel(SupportedShapes kind, int id, Point? boundary = null)
    {
        this.Id = id;
        this.Kind = kind;
        this.Boundary = boundary ?? new Point();

        this.LocalizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.Name));

        (this.X, this.Y) = PointRandomizer.Next(this.Boundary);
    }

    public Point Boundary { get; set; }

    public Point Velocity { get; set; } = new(10, 10);

    public double X
    {
        get => this.x;
        set
        {
            if (value < 0 || value > this.Boundary.X)
            {
                throw new ShapeOutOfBoundsException(this);
            }

            this.SetProperty(ref this.x, value);
        }
    }

    public double Y
    {
        get => this.y;
        set
        {
            if (value < 0 || value > this.Boundary.Y)
            {
                throw new ShapeOutOfBoundsException(this);
            }

            this.SetProperty(ref this.y, value);
        }
    }

    public int Id { get; }

    public SupportedShapes Kind { get; }

    public string Name => $"{this.Kind.ToLocalizedString()} {this.Id}";

    private Point NextPoint => new(this.X + this.Velocity.X, this.Y + this.Velocity.Y);

    public void Move()
    {
        if (this.IsPaused)
        {
            return;
        }

        var nextPoint = this.NextPoint;
        if (nextPoint.X <= 0 || nextPoint.X >= this.Boundary.X)
        {
            this.Velocity.X = Bounce(this.Velocity.X);
            this.Velocity.X *= -1;
        }

        if (nextPoint.Y <= 0 || nextPoint.Y >= this.Boundary.Y)
        {
            this.Velocity.Y = Bounce(this.Velocity.Y);
            this.Velocity.Y *= -1;
        }

        this.Velocity.Y = Friction(this.Velocity.Y);
        this.Velocity.X = Friction(this.Velocity.X);

        (this.X, this.Y) = this.NextPoint;
    }

    private static double Friction(double value) =>
        double.Abs(value) > 10
            ? (double.Abs(value) - .5) * double.Sign(value)
            : value;

    private static double Bounce(double value) =>
        double.Clamp(
            (10 + double.Abs(value)) * double.Sign(value),
            -30,
            30);

    internal void JumpToBoundary()
    {
        this.X = double.Clamp(this.X, 0, this.Boundary.X);
        this.Velocity.X = Bounce(this.Velocity.X);
        this.Y = double.Clamp(this.Y, 0, this.Boundary.Y);
        this.Velocity.Y = Bounce(this.Velocity.Y);
    }
}