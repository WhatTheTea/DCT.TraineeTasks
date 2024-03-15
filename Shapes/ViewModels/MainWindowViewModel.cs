﻿// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
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
    private readonly LocalizerService LocalizedStrings = Locator.Current.GetService<LocalizerService>()
                                                                ?? throw new ArgumentNullException(nameof(LocalizedStrings));

    private readonly ShapeViewFactory shapeFactory = Locator.Current.GetService<ShapeViewFactory>()
                                                          ?? throw new ArgumentNullException(nameof(shapeFactory));

    private ReadOnlyObservableCollection<MovingShape> movingShapesViews;

    public MainWindowViewModel()
    {
        this.MovingShapes = new();

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
                    shape.ViewModel.Boundary = this.Boundary;
                    shape.ViewModel.Move.Execute();
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

                shape.ViewModel.PlayPause.Execute();
                // TODO: Remove
                this.IsSelectedShapePaused = shape.ViewModel.IsPaused;
            });

        this.ChangeLanguage = ReactiveCommand.Create<CultureInfo, Unit>(
            culture =>
            {
                this.LocalizedStrings.CurrentCulture = culture;

                // collections binding skill issue ;-;
                // this.MovingShapesNames = this.SelectMovingShapesNames(this.MovingShapes.AsReadOnly());
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


        // VMs to Names
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(x => x.Select(shape => this.MovingShapesNamesDictionary[shape.Id]))
            .ToPropertyEx(this, x => x.MovingShapesNames);
        // SelectedShapeName -> SelectedShape
        this.WhenAnyValue(x => x.SelectedShapeName, y => y.LocalizedStrings.CurrentCulture)
            .Select(x=>
            {
                if (this.MovingShapesNamesDictionary == null || this.MovingShapesNamesDictionary.Count < 1)
                {
                    return null;
                }

                var pair = this.MovingShapesNamesDictionary.FirstOrDefault(y => y.Value == (x.Item1 ?? string.Empty));
                return this.MovingShapes.FirstOrDefault(y => y.Id == pair.Key);
                })
            .ToPropertyEx(this, x => x.SelectedShape);

        // On ShapeSelection and locale change set PlayButtonText
        this.WhenAnyValue(
                x => x.SelectedShape,
                x => x.LocalizedStrings.CurrentCulture,
                x => x.IsSelectedShapePaused)
            .Select(x => this.GetPlayButtonTextFor(x.Item1))
            .ToPropertyEx(this, x => x.PlayButtonText);

        this.LocalizedStrings.CurrentCulture = CultureInfo.CurrentCulture;
        // TriangleText
        this.WhenAnyValue(x => x.LocalizedStrings.Triangle)
            .ToPropertyEx(this, x => x.TriangleText);

        // CircleText
        this.WhenAnyValue(x => x.LocalizedStrings.Circle)
            .ToPropertyEx(this, x => x.CircleText);

        // SquareText
        this.WhenAnyValue(x => x.LocalizedStrings.Square)
            .ToPropertyEx(this, x => x.SquareText);

        // Moving shapes -> shapes names dictionary
        this.MovingShapes
            .ToObservableChangeSet(x => x)
            .ToCollection()
            .Select(x =>
            {
                var shapeCounts = new Dictionary<string, int>();
                var pairs = x.Select(
                    shape =>
                    {
                        var isValue = shapeCounts.TryGetValue(shape.Name, out var count);
                        if (!isValue)
                        {
                            shapeCounts[shape.Name] = 1;
                        }

                        shapeCounts[shape.Name]++;

                        return new KeyValuePair<Guid, string>(shape.Id, $"{shape.Name} {count + 1}");
                    });
                return new Dictionary<Guid, string>(pairs);
            })
            .ToPropertyEx(this, x => x.MovingShapesNamesDictionary);
    }

#pragma warning disable SA1600 // ElementsMustBeDocumented

    // [ObservableAsProperty]
    public ReadOnlyObservableCollection<MovingShape> MovingShapesViews
    {
        get => this.movingShapesViews;
        set => this.RaiseAndSetIfChanged(ref this.movingShapesViews, value);
    }

    [Reactive]
    public ObservableCollection<MovingShape> MovingShapes { get; private set; }

    [ObservableAsProperty] 
    public IEnumerable<string> MovingShapesNames { get; set; }

    [ObservableAsProperty] 
    public Dictionary<Guid, string> MovingShapesNamesDictionary { get; } = new();

    [Reactive] 
    public string? SelectedShapeName { get; set; }
    
    [Reactive]
    public bool IsSelectedShapePaused { get; set; }

    [ObservableAsProperty]
    public MovingShape? SelectedShape { get; }

    [Reactive]
    public Point Boundary { get; set; } = new(300, 300);

    [ObservableAsProperty] public string PlayButtonText { get; }

    [ObservableAsProperty] public string CircleText { get; } = string.Empty;

    [ObservableAsProperty] public string SquareText { get; } = string.Empty;

    [ObservableAsProperty] public string TriangleText { get; } = string.Empty;

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

    private string GetPlayButtonTextFor(MovingShape? shape)
    {
        if (shape == null)
        {
            return this.LocalizedStrings.PlayButtonSelect;
        }

        return shape.ViewModel.IsPaused ? this.LocalizedStrings.PlayButtonPlay : this.LocalizedStrings.PlayButtonPause;
    }
}