// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Shapes.Resources;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public partial class ShapeViewModel : ObservableObject
{
    private readonly RelayCommand moving;

    private readonly RelayCommand paused = new(
        () =>
        {
            // Do nothing
        });

    [ObservableProperty] private ICommand move;

    [ObservableProperty] private string name;
    [ObservableProperty] private double x;
    [ObservableProperty] private double y;

    public ShapeViewModel(int id, string name, SupportedShapes kind)
    {
        this.Id = id;
        this.ShapeKind = kind;
        this.name = name;

        this.moving = new RelayCommand(() => { (this.X, this.Y) = (this.X + 10, this.Y + 10); });

        this.Move = this.moving;
    }

    public int Id { get; }

    public SupportedShapes ShapeKind { get; }

    public bool IsPaused => this.Move == this.paused;
}