// <copyright file="MainViewModelTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Events;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class MainViewModelTests
{
    private MainViewModel ViewModel { get; set; }
    
    [SetUp]
    public void Setup()
    {
        this.ViewModel = new MainViewModel
        {
            CanvasHeight = 100,
            CanvasWidth = 100,
        };
    }
    
    [Test]
    [Category("Intersection")]
    public void IntersectionSameShapes()
    {
        // Arrange
        using var vm = this.ViewModel.Monitor();
        var shape1 = new ShapeViewModel(0, 0) { X = 10, Y = 10, };
        var shape2 = new ShapeViewModel(0, 1) { X = 10, Y = 10, };
        this.ViewModel.AddShape(shape1);
        this.ViewModel.AddShape(shape2);
        this.ViewModel.AddEventHandlerToCommand.Execute(shape1);
        this.ViewModel.AddEventHandlerToCommand.Execute(shape2);

        // Act
        this.ViewModel.MoveShapes();

        // Assert
        vm.Should().Raise(nameof(MainViewModel.IntersectionOccured))
            .WithArgs<IntersectionEventArgs>(
                x => x.Shape1 == shape1
                && x.Shape2 == shape2);

        vm.Should().Raise(nameof(MainViewModel.IntersectionOccured))
            .WithArgs<IntersectionEventArgs>(
                x => x.Shape1 == shape2
                     && x.Shape2 == shape1);
    }

    [Test]
    [Category("Intersection")]
    public void IntersectionDifferentKinds()
    {
        // Arrange
        using var vm = this.ViewModel.Monitor();
        var shape1 = new ShapeViewModel(SupportedShapes.Circle, 0) { X = 10, Y = 10, };
        var shape2 = new ShapeViewModel(SupportedShapes.Square, 1) { X = 10, Y = 10, };
        this.ViewModel.AddShape(shape1);
        this.ViewModel.AddShape(shape2);
        this.ViewModel.AddEventHandlerToCommand.Execute(shape1);
        this.ViewModel.AddEventHandlerToCommand.Execute(shape2);

        // Act
        this.ViewModel.MoveShapes();

        // Assert
        vm.Should().NotRaise(nameof(MainViewModel.IntersectionOccured));
    }

    [Test]
    [Category("Intersection")]
    public void IntersectionDifferentPositions()
    {
        // Arrange
        using var vm = this.ViewModel.Monitor();
        var shape1 = new ShapeViewModel(0, 0) { X = 10, Y = 10, };
        var shape2 = new ShapeViewModel(0, 1) { X = 20, Y = 10, };
        this.ViewModel.AddShape(shape1);
        this.ViewModel.AddShape(shape2);
        this.ViewModel.AddEventHandlerToCommand.Execute(shape1);
        this.ViewModel.AddEventHandlerToCommand.Execute(shape2);

        // Act
        this.ViewModel.MoveShapes();

        // Assert
        vm.Should().NotRaise(nameof(MainViewModel.IntersectionOccured));
    }
    
    [Test]
    [Category("PlayButton")]
    public void PlayButtonPause()
    {
        // Arrange
        var localizedStrings = Ioc.Default.GetService<LocalizerService>() !;
        localizedStrings.Should().NotBeNull();
        var vm = this.ViewModel;
        var shape = new ShapeViewModel(0, 0);
        vm.AddShape(shape);
        vm.SelectedShape = shape;

        // Act
        shape.IsPaused = false;

        // Assert
        vm.ButtonText.Should().Be(localizedStrings.PlayButtonPause);
    }
}