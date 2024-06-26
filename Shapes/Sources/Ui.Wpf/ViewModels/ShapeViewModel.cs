﻿// <copyright file = "ShapeViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Randomizer;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Exceptions;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

public partial class ShapeViewModel : ObservableObject
{
    [ObservableProperty] private bool _isPaused;

    private double _x;
    private double _y;

    public ShapeViewModel(SupportedShapes kind, int id, Point? boundary = null)
    {
        this.Id = id;
        this.Kind = kind;
        this.Boundary = boundary ?? new Point();

        this.Localization.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(this.Localization.UiCulture))
            {
                this.OnPropertyChanged(nameof(this.Name));
            }
        };

        (this.X, this.Y) = PointRandomizer.Next(this.Boundary);
    }

    internal ILocalizationManager Localization { get; } = Ioc.Default.GetService<ILocalizationManager>()
                                                          ?? throw new ArgumentNullException(nameof(Localization));

    public Point Boundary { get; set; }

    public Point Velocity { get; set; } = new(10, 10);

    public double X
    {
        get => this._x;
        set
        {
            if (value < 0 || value > this.Boundary.X)
            {
                throw new ShapeOutOfBoundsException(this);
            }

            this.SetProperty(ref this._x, value);
        }
    }

    public double Y
    {
        get => this._y;
        set
        {
            if (value < 0 || value > this.Boundary.Y)
            {
                throw new ShapeOutOfBoundsException(this);
            }

            this.SetProperty(ref this._y, value);
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

        Point nextPoint = this.NextPoint;
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
