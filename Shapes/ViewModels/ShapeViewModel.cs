// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCT.TraineeTasks.Shapes.Resources;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public partial class ShapeViewModel : ObservableObject
{
    private readonly Action moving;

    private readonly Action paused = () =>
    {
        // Do nothing
    };

    [ObservableProperty] private Action move;
    [ObservableProperty] private string name;
    [ObservableProperty] private double x;
    [ObservableProperty] private double y;

    public ShapeViewModel(int id, string name, SupportedShapes kind)
    {
        this.Id = id;
        this.ShapeKind = kind;
        this.name = name;

        this.moving = () => { (this.X, this.Y) = (this.X + 10, this.Y + 10); };

        this.Move = this.moving;
    }

    public int Id { get; }

    public SupportedShapes ShapeKind { get; }

    public bool IsPaused => this.Move == this.paused;

    [RelayCommand]
    private void PauseToggle()
    {
        this.Move = this.IsPaused ? this.moving : this.paused;
    }
}