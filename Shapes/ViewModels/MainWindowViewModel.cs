// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>


namespace DCT.TraineeTasks.Shapes.ViewModels;

using System.Windows;
using System.Reactive;
using System.Windows.Shapes;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI.Fody.Helpers;
using MovingShapes;


using ReactiveUI;

public class MainWindowViewModel : ReactiveObject
{
    public ObservableCollection<MovingShape> MovingShapes { get; set; } = new();

    [ObservableAsProperty]
    public IEnumerable<string> MovingShapesNames { get; }
    
    [Reactive]
    public MovingShape? SelectedShape { get; set; }

    [Reactive] public Point Boundary { get; set; } = new(300, 300);
    
    [Reactive]
    public string PlayButtonText { get; set; }
    [Reactive]
    public string CircleText { get; set; }
    [Reactive]
    public string SquareText { get; set; }
    [Reactive]
    public string TriangleText { get; set; }
    
    public ReactiveCommand<Unit, Unit> MoveShapes { get; set; }
    public ReactiveCommand<Unit, Unit> AddSquare { get; set; }
    public ReactiveCommand<Unit, Unit> AddTriangle { get; set; }
    public ReactiveCommand<Unit, Unit> AddCircle { get; set; }
    public ReactiveCommand<Unit, Unit> PauseUnPause { get; set; }
    public ReactiveCommand<string, Unit> ChangeLanguage { get; set; }
    public ReactiveCommand<Unit,Unit> TimerTick { get; set; }

    public MainWindowViewModel()
    {
        this.AddCircle = ReactiveCommand.Create(() => this.MovingShapes.Add(new MovingCircle(this.Boundary)));
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(x => x.Select(y => y.ToString()))
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToPropertyEx(this, x => x.MovingShapesNames);
        this.WhenAnyValue(x => x.MovingShapesNames)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(x => Console.WriteLine(x?.LastOrDefault() ?? ""));
        this.TimerTick = ReactiveCommand.Create(
            () =>
            {
                foreach (var shape in MovingShapes)
                {
                    shape.Boundary = this.Boundary;
                    shape.Move();
                }
            });
    }
}