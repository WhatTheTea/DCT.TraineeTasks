// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reactive;
using System.Windows;
using DCT.TraineeTasks.Shapes.Models.MovingShapes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public abstract class ShapeViewModel : ReactiveObject
{
    protected static LocalizerService LocalizedStrings = Locator.Current.GetService<LocalizerService>()
        ?? throw new ArgumentNullException(nameof(LocalizedStrings));

    public int Id { get; }

    public MovingShape ShapeView { get; }

    [ObservableAsProperty]
    public Point RightDownBoundary { get; }

    [ObservableAsProperty]
    public bool IsPaused { get; }

    [ObservableAsProperty]
    public string Name { get; }

    [Reactive]
    public Point Offset { get; set; }

    public ReactiveCommand<Unit, Unit> Move { get; }

    protected ReactiveCommand<Unit, Unit> MoveActive = ReactiveCommand
        .Create<Unit, Unit>(_ => default);

    protected ReactiveCommand<Unit, Unit> MovePaused = ReactiveCommand
        .Create<Unit, Unit>(_ => default);
}