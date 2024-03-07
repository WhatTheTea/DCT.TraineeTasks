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

    [ObservableAsProperty] public IEnumerable<string> MovingShapesNames { get; }

    [Reactive] public string SelectedShapeName { get; set; }

    [ObservableAsProperty] public MovingShape? SelectedShape { get; }

    [Reactive] public Point Boundary { get; set; } = new(300, 300);

    [Reactive] public string PlayButtonText { get; set; }

    [Reactive] public string CircleText { get; set; }

    [Reactive] public string SquareText { get; set; }

    [Reactive] public string TriangleText { get; set; }

    public ReactiveCommand<Unit, Unit> AddSquare { get; set; }

    public ReactiveCommand<Unit, Unit> AddTriangle { get; set; }

    public ReactiveCommand<Unit, Unit> AddCircle { get; set; }

    public ReactiveCommand<Unit, Unit> PlayPause { get; set; }

    public ReactiveCommand<string, Unit> ChangeLanguage { get; set; }

    public ReactiveCommand<Unit, Unit> MoveShapes { get; set; }

    public MainWindowViewModel()
    {
        // Moving shapes -> shapes names
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(x => x.Select(y => y.ToString()))
            .ToPropertyEx(this, x => x.MovingShapesNames);

        // SelectedShapeName -> SelectedShape
        this.WhenAnyValue(x => x.SelectedShapeName)
            .Select(x => this.MovingShapes.FirstOrDefault(shape => shape.ToString() == x))
            .ToPropertyEx(this, x => x.SelectedShape);

        // On ShapeSelection set PlayButtonText
        this.WhenAnyValue(x => x.SelectedShape)
            .Select(this.GetPlayButtonTextFor)
            .Subscribe(x => this.PlayButtonText = x);

        this.AddCircle = ReactiveCommand.Create(() => this.MovingShapes.Add(new MovingCircle(this.Boundary)));
        this.AddSquare = ReactiveCommand.Create(() => this.MovingShapes.Add(new MovingRectangle(this.Boundary)));
        this.AddTriangle = ReactiveCommand.Create(() => this.MovingShapes.Add(new MovingTriangle(this.Boundary)));
        this.MoveShapes = ReactiveCommand.Create(
            () =>
            {
                foreach (var shape in this.MovingShapes)
                {
                    shape.Boundary = this.Boundary;
                    shape.Move();
                }
            });

        this.PlayPause = ReactiveCommand.Create(
            () =>
            {
                if (this.SelectedShape == null)
                {
                    return;
                }

                if (this.SelectedShape.IsPaused)
                {
                    this.SelectedShape.UnPause();
                }
                else
                {
                    this.SelectedShape.Pause();
                }

                this.PlayButtonText = this.GetPlayButtonTextFor(this.SelectedShape);
            });
    }

    private string GetPlayButtonTextFor(MovingShape? shape)
    {
        if (shape == null)
        {
            return "Select item";
        }

        return shape.IsPaused ? "Play" : "Pause";
    }
}