// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using DCT.TraineeTasks.Shapes.Models.MovingShapes;
using DCT.TraineeTasks.Shapes.ViewModels.Shapes;
using DCT.TraineeTasks.Shapes.Views;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    private static readonly LocalizerService LocalizedStrings = Locator.Current.GetService<LocalizerService>()
                                                                ?? throw new ArgumentNullException(nameof(LocalizedStrings));

    private readonly ShapeViewModelFactory shapeFactory = Locator.Current.GetService<ShapeViewModelFactory>()
                                                          ?? throw new ArgumentNullException(nameof(shapeFactory));
    public MainWindowViewModel()
    {
        this.AddCircle = ReactiveCommand.Create(
            () => this.MovingShapes.Add(
                this.shapeFactory.MakeCircle()));

        this.AddSquare = ReactiveCommand.Create(
            () => this.MovingShapes.Add(
                this.shapeFactory.MakeSquare()));

        this.AddTriangle = ReactiveCommand.Create(
            () => this.MovingShapes.Add(
                this.shapeFactory.MakeTriangle()));

        this.MoveShapes = ReactiveCommand.Create(
            () =>
            {
                foreach (var shape in this.MovingShapes)
                {
                    shape.Boundary = this.Boundary;
                    shape.Move.Execute();
                }
            });

        this.PlayPause = ReactiveCommand.Create( // selectedShape.PlayPause();
            () =>
            {
                var shape = this.SelectedShape;
                if (shape == null)
                {
                    return;
                }

                shape.PlayPause.Execute();
                // TODO: Remove
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
                        throw new NotImplementedException();
                        // service.Save(this.MovingShapes);
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
        // Cache shapes by guid
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(x => x.Select(y => new KeyValuePair<Guid, ShapeViewModel>(y.Shape.Id, y)))
            .BindTo(this, x => x.MovingShapesDictionary);

        // SelectedShapeName -> SelectedShape
        this.WhenAnyValue(x => x.SelectedShapeName, y => y.CurrentCulture)
            .Select(x => this.MovingShapes.Where((shape, i) => shape.Name + i == x.Item1).FirstOrDefault())
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
    public ObservableCollection<ShapeViewModel> MovingShapes { get; private set; } = new();

    [ObservableAsProperty]
    public ReadOnlyDictionary<Guid, ShapeViewModel> MovingShapesDictionary { get; }
    
    [Reactive]
    public IEnumerable<string> MovingShapesNames { get; set; }

    [Reactive]
    public string SelectedShapeName { get; set; }
    
    [Reactive]
    public bool IsSelectedShapePaused { get; set; }

    [ObservableAsProperty]
    public ShapeViewModel? SelectedShape { get; }

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

    private IEnumerable<string> SelectMovingShapesNames(IReadOnlyCollection<ShapeViewModel> x)
    {
        return x.Select((shape, i) => shape.Name + i);
    }

    private string GetPlayButtonTextFor(ShapeViewModel? shape)
    {
        if (shape == null)
        {
            return LocalizedStrings.PlayButtonSelect;
        }

        return shape.IsPaused ? LocalizedStrings.PlayButtonPlay : LocalizedStrings.PlayButtonPause;
    }
}