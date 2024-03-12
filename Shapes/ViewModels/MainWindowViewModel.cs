// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DCT.TraineeTasks.Shapes.Models.MovingShapes;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private static readonly LocalizerService LocalizedStrings = Locator.Current.GetService<LocalizerService>() !;

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

                // collections binding skill issue ;-;
                this.MovingShapesNames = this.SelectMovingShapesNames(this.MovingShapes.AsReadOnly());
                return default;
            });

        this.Save = ReactiveCommand.Create<FileFormats, Unit>(
            format =>
            {
                // temp
                var service = new BinaryFileService();
                switch (format)
                {
                    case FileFormats.Bin:
                        service.Save(this.MovingShapes);
                        break;
                }

                return default;
            });

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
            .ToPropertyEx(this, x => x.PlayButtonText);

        // TriangleText
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(x => LocalizedStrings.Triangle)
            .ToPropertyEx(this, x => x.TriangleText);

        // CircleText
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(x => LocalizedStrings.Circle)
            .ToPropertyEx(this, x => x.CircleText);

        // SquareText
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(x => LocalizedStrings.Square)
            .ToPropertyEx(this, x => x.SquareText);

        this.CurrentCulture = CultureInfo.CurrentCulture;
    }

#pragma warning disable SA1600 // ElementsMustBeDocumented

    [Reactive]
    public CultureInfo CurrentCulture { get; set; }

    [Reactive]
    public ObservableCollection<MovingShape> MovingShapes { get; private set; } = new();

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

    [ObservableAsProperty] public string PlayButtonText { get; }

    [ObservableAsProperty] public string CircleText { get; } = LocalizedStrings.Circle;

    [ObservableAsProperty] public string SquareText { get; } = LocalizedStrings.Square;

    [ObservableAsProperty] public string TriangleText { get; } = LocalizedStrings.Triangle;

    public ReactiveCommand<Unit, Unit> AddSquare { get; }

    public ReactiveCommand<Unit, Unit> AddTriangle { get; }

    public ReactiveCommand<Unit, Unit> AddCircle { get; }

    public ReactiveCommand<Unit, Unit> PlayPause { get; }
    
    public ReactiveCommand<FileFormats, Unit> Save { get; }

    public ReactiveCommand<CultureInfo, Unit> ChangeLanguage { get; }

    public ReactiveCommand<Unit, Unit> MoveShapes { get; }

#pragma warning restore SA1600 // ElementsMustBeDocumented

    private IEnumerable<string> SelectMovingShapesNames(IReadOnlyCollection<MovingShape> x)
    {
        return x.Select(y => y.ToString() ?? string.Empty);
    }

    private string GetPlayButtonTextFor(MovingShape? shape)
    {
        if (shape == null)
        {
            return LocalizedStrings.PlayButtonSelect;
        }

        return shape.IsPaused ? LocalizedStrings.PlayButtonPlay : LocalizedStrings.PlayButtonPause;
    }
}