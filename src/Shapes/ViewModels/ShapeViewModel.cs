// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using DCT.TraineeTasks.Shapes.Converters;
using DCT.TraineeTasks.Shapes.Primitives;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public partial class ShapeViewModel : ObservableObject
{
    private readonly LocalizerServiceObservableWrapper localizerService =
        App.Current.Services.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(localizerService));

    [ObservableProperty] private bool isPaused;
    [ObservableProperty] private double x;
    [ObservableProperty] private double y;

    public ShapeViewModel(SupportedShapes kind, int id, Point? boundary = null)
    {
        this.Id = id;
        this.ShapeKind = kind;
        this.Boundary = boundary ?? new Point();

        this.localizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.Name));

        this.X = Random.Shared.Next(0, (int)this.Boundary.X);
        this.Y = Random.Shared.Next(0, (int)this.Boundary.Y);
    }

    public Point Boundary { get; set; }

    public Point Velocity { get; set; } = new(10, 10);

    public int Id { get; }

    public SupportedShapes ShapeKind { get; }

    public string Name => $"{this.ShapeKind.ToLocalizedString()} {this.Id}";

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
            this.Velocity = new Point(this.Velocity.X * -1, this.Velocity.Y);
        }

        if (nextPoint.Y <= 0 || nextPoint.Y >= this.Boundary.Y)
        {
            this.Velocity = new Point(this.Velocity.X, this.Velocity.Y * -1);
        }

        (this.X, this.Y) = this.NextPoint;
    }
}