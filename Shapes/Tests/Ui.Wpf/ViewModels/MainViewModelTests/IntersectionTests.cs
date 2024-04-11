// <copyright file = "IntersectionTests.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Events;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class IntersectionTests : Base
{
    public override void Setup()
    {
        base.Setup();
        ShapeViewModel shape1 = new(0, 0, this.MonitoredViewModel.Subject.CanvasBoundary) { X = 10, Y = 10 };
        ShapeViewModel shape2 = new(0, 1, this.MonitoredViewModel.Subject.CanvasBoundary) { X = 10, Y = 10 };
        this.MonitoredViewModel.Subject.AddShape(shape1);
        this.MonitoredViewModel.Subject.AddShape(shape2);
        this.MonitoredViewModel.Subject.AddEventHandlerToCommand.Execute(shape1);
        this.MonitoredViewModel.Subject.AddEventHandlerToCommand.Execute(shape2);
    }

    [Test]
    public void IntersectionSameShapes()
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;

        // Act
        vm.MoveShapes();

        // Assert
        this.MonitoredViewModel.Should().Raise(nameof(MainViewModel.IntersectionOccured))
            .WithArgs<IntersectionEventArgs>(
                x => x.Shape1 == vm.Shapes[0]
                     && x.Shape2 == vm.Shapes[1]);

        this.MonitoredViewModel.Should().Raise(nameof(MainViewModel.IntersectionOccured))
            .WithArgs<IntersectionEventArgs>(
                x => x.Shape1 == vm.Shapes[1]
                     && x.Shape2 == vm.Shapes[0]);
    }

    [Test]
    public void IntersectionDifferentKinds()
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;
        vm.Shapes.RemoveAt(1);
        vm.AddShape(new ShapeViewModel(SupportedShapes.Square, 1));

        // Act
        vm.MoveShapes();

        // Assert
        this.MonitoredViewModel.Should().NotRaise(nameof(MainViewModel.IntersectionOccured));
    }

    [Test]
    public void IntersectionDifferentPositions()
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;
        vm.Shapes[1].Move();

        // Act
        vm.MoveShapes();

        // Assert
        this.MonitoredViewModel.Should().NotRaise(nameof(MainViewModel.IntersectionOccured));
    }

    [Test]
    public void IntersectionSeveralInvocations()
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;
        vm.AddEventHandlerToCommand.Execute(vm.Shapes[0]);
        const int expectedInvocations = 3;

        // Act
        vm.MoveShapes();

        // Assert
        this.MonitoredViewModel
            .OccurredEvents
            .Should()
            .HaveCount(expectedInvocations);
    }

    [Test]
    public void RemoveInvocation()
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;
        ShapeViewModel shapeNoInvoke = vm.Shapes[0];

        // Act
        vm.RemoveHandlerFromCommand.Execute(shapeNoInvoke);
        vm.MoveShapes();

        // Assert
        this.MonitoredViewModel
            .OccurredEvents
            .Should()
            .HaveCount(1);
    }
}
