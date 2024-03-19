﻿// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Shapes.Converters;
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

    public ShapeViewModel(SupportedShapes kind, int id, (double x, double y) boundary = default)
    {
        this.Id = id;
        this.ShapeKind = kind;
        this.SetBoundary(boundary.x, boundary.y);

        this.localizerService.PropertyChanged += (_, _) =>
            this.OnPropertyChanged(nameof(this.Name));

        this.X = Random.Shared.Next(0, (int)this.Boundary.x);
        this.Y = Random.Shared.Next(0, (int)this.Boundary.y);
    }

    private (double x, double y) Boundary { get; set; } = (0, 0);

    private (double x, double y) Velocity { get; set; } = (10, 10);

    public int Id { get; }

    public SupportedShapes ShapeKind { get; }

    public string Name => $"{this.ShapeKind.ToLocalizedString()} {this.Id}";

    private (double x, double y) NextPoint => (this.X + this.Velocity.x, this.Y + this.Velocity.y);

    public void SetBoundary(double x, double y) => this.Boundary = (x, y);

    public void Move()
    {
        if (this.IsPaused)
        {
            return;
        }

        var nextPoint = this.NextPoint;
        if (nextPoint.x <= 0 || nextPoint.x >= this.Boundary.x)
        {
            this.Velocity = (this.Velocity.x * -1, this.Velocity.y);
        }

        if (nextPoint.y <= 0 || nextPoint.y >= this.Boundary.y)
        {
            this.Velocity = (this.Velocity.x, this.Velocity.y * -1);
        }

        (this.X, this.Y) = this.NextPoint;
    }
}