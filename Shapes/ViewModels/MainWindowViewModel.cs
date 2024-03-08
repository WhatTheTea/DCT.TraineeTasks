// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.Shapes.ViewModels;

using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using MovingShapes;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

public class MainWindowViewModel : ReactiveObject
{
    private LocalizerService Localizer = Locator.Current.GetService<LocalizerService>() !;
    public MainWindowViewModel()
    {
        // Moving shapes -> shapes names
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(this.SelectMovingShapesNames)
            .BindTo(this, x => x.MovingShapesNames);

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

        this.ChangeLanguage = ReactiveCommand.Create<CultureInfo, Unit>(
            culture =>
            {
                this.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = this.CurrentCulture;

                this.MovingShapesNames = this.SelectMovingShapesNames(this.MovingShapes.AsReadOnly());
                this.PlayButtonText = this.GetPlayButtonTextFor(this.SelectedShape);
                this.SetShapesText();

                return default;
            });

        this.SetShapesText();
    }

    private void SetShapesText()
    {
        this.TriangleText = this.Localizer.Triangle;
        this.CircleText = this.Localizer.Circle;
        this.SquareText = this.Localizer.Square;
    }

    [Reactive] public CultureInfo CurrentCulture { get; set; } = CultureInfo.CurrentUICulture;

    public ObservableCollection<MovingShape> MovingShapes { get; set; } = new();

    [Reactive] public IEnumerable<string> MovingShapesNames { get; set; }

    [Reactive] public string SelectedShapeName { get; set; }

    [ObservableAsProperty] public MovingShape? SelectedShape { get; }

    [Reactive] public Point Boundary { get; set; } = new(300, 300);

    [Reactive] public string PlayButtonText { get; set; }

    [Reactive] public string CircleText { get; set; }

    [Reactive] public string SquareText { get; set; }

    [Reactive] public string TriangleText { get; set; }

    public ReactiveCommand<Unit, Unit> AddSquare { get; }

    public ReactiveCommand<Unit, Unit> AddTriangle { get; }

    public ReactiveCommand<Unit, Unit> AddCircle { get; }

    public ReactiveCommand<Unit, Unit> PlayPause { get; }

    public ReactiveCommand<CultureInfo, Unit> ChangeLanguage { get; }

    public ReactiveCommand<Unit, Unit> MoveShapes { get; }

    private IEnumerable<string> SelectMovingShapesNames(IReadOnlyCollection<MovingShape> x)
    {
        return x.Select(y => y.ToString() !);
    }

    private string GetPlayButtonTextFor(MovingShape? shape)
    {
        if (shape == null)
        {
            return this.Localizer.PlayButtonSelect;
        }

        return shape.IsPaused ? this.Localizer.PlayButtonPlay : this.Localizer.PlayButtonPause;
    }
}