// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>


namespace DCT.TraineeTasks.Shapes.ViewModels;

using System.Windows;
using System.Reactive;
using System.Windows.Shapes;
using ReactiveUI.Fody.Helpers;
using MovingShapes;


using ReactiveUI;

public class MainWindowViewModel : ReactiveObject
{
    public List<MovingShape> MovingShapes { get; set; }
    
    [Reactive]
    public MovingShape? SelectedShape { get; set; }
    
    [Reactive]
    public Point Boundary { get; set; }
    
    public ReactiveCommand<Unit, Unit> MoveShapes { get; set; }
    public ReactiveCommand<Unit, Unit> AddSquare { get; set; }
    public ReactiveCommand<Unit, Unit> AddTriangle { get; set; }
    public ReactiveCommand<Unit, Unit> AddCircle { get; set; }
}