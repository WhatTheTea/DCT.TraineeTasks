// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DCT.TraineeTasks.Shapes.MovingShapes;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private readonly LocalizerService LocalizedStrings = Locator.Current.GetService<LocalizerService>() !;

    public MainWindowViewModel()
    {
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
                var shape = this.SelectedShape;
                if (shape == null)
                {
                    return;
                }

                if (shape.IsPaused)
                {
                    shape.UnPause();
                }
                else
                {
                    shape.Pause();
                }

                this.IsSelectedShapePaused = shape.IsPaused;
            });

        this.ChangeLanguage = ReactiveCommand.Create<CultureInfo, Unit>(
            culture =>
            {
                Thread.CurrentThread.CurrentUICulture = culture;
                this.CurrentCulture = culture;

                this.MovingShapesNames = this.SelectMovingShapesNames(this.MovingShapes.AsReadOnly());
                this.SetShapesText();
                return default;
            });

        this.SetShapesText();
        // TODO : OAPH
        // Moving shapes -> shapes names
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(this.SelectMovingShapesNames)
            .BindTo(this, x => x.MovingShapesNames);

        // SelectedShapeName -> SelectedShape
        this.WhenAnyValue(x => x.SelectedShapeName, y => y.CurrentCulture)
            .Select(x => this.MovingShapes.FirstOrDefault(shape => shape.ToString() == x.Item1))
            .ToPropertyEx(this, x => x.SelectedShape);

        // On ShapeSelection and locale change set PlayButtonText
        this.WhenAnyValue(
                x => x.SelectedShape,
                x => x.CurrentCulture,
                x => x.IsSelectedShapePaused)
            .Select(x => this.GetPlayButtonTextFor(x.Item1))
            .Do(x => Debug.WriteLine("On shape selection: " + x))
            .ToPropertyEx(this, x => x.PlayButtonText);
            }

    [Reactive]
    public CultureInfo CurrentCulture { get; set; }

    public ObservableCollection<MovingShape> MovingShapes { get; set; } = new();

    [Reactive]
    public IEnumerable<string> MovingShapesNames { get; set; }

    [Reactive]
    public string SelectedShapeName { get; set; }
    
    [Reactive]
    public bool IsSelectedShapePaused { get; set; }

    [ObservableAsProperty]
    public MovingShape? SelectedShape { get; }

    [Reactive]
    public Point Boundary { get; set; } = new(300, 300);

    [ObservableAsProperty]
    public string PlayButtonText { get; }

    [Reactive] public string CircleText { get; set; }

    [Reactive] public string SquareText { get; set; }

    [Reactive] public string TriangleText { get; set; }

    public ReactiveCommand<Unit, Unit> AddSquare { get; }

    public ReactiveCommand<Unit, Unit> AddTriangle { get; }

    public ReactiveCommand<Unit, Unit> AddCircle { get; }

    public ReactiveCommand<Unit, Unit> PlayPause { get; }

    public ReactiveCommand<CultureInfo, Unit> ChangeLanguage { get; }

    public ReactiveCommand<Unit, Unit> MoveShapes { get; }

    private void SetShapesText()
    {
        this.TriangleText = this.LocalizedStrings.Triangle;
        this.CircleText = this.LocalizedStrings.Circle;
        this.SquareText = this.LocalizedStrings.Square;
    }

    private IEnumerable<string> SelectMovingShapesNames(IReadOnlyCollection<MovingShape> x)
    {
        return x.Select(y => y.ToString() !);
    }

    private string GetPlayButtonTextFor(MovingShape? shape)
    {
        if (shape == null)
        {
            return this.LocalizedStrings.PlayButtonSelect;
        }

        return shape.IsPaused ? this.LocalizedStrings.PlayButtonPlay : this.LocalizedStrings.PlayButtonPause;
    }
}