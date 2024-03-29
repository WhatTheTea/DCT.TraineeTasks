// <copyright file="IntersectionTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Events;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class IntersectionTests : Base
{
    public override void Setup()
    {
        base.Setup();
        var shape1 = new ShapeViewModel(0, 0, this.MonitoredViewModel.Subject.CanvasBoundary)
            { X = 10, Y = 10 };
        var shape2 = new ShapeViewModel(0, 1, this.MonitoredViewModel.Subject.CanvasBoundary)
            { X = 10, Y = 10 };
        this.MonitoredViewModel.Subject.AddShape(shape1);
        this.MonitoredViewModel.Subject.AddShape(shape2);
        this.MonitoredViewModel.Subject.AddEventHandlerToCommand.Execute(shape1);
        this.MonitoredViewModel.Subject.AddEventHandlerToCommand.Execute(shape2);
    }

    [Test]
    public void IntersectionSameShapes()
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;

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
        var vm = this.MonitoredViewModel.Subject;
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
        var vm = this.MonitoredViewModel.Subject;
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
        var vm = this.MonitoredViewModel.Subject;
        var invokingShape = vm.Shapes[0];
        vm.AddEventHandlerToCommand.Execute(invokingShape);
        var invokeCount = 0;

        // Act
        vm.IntersectionOccured += (_, args) =>
        {
            if (args.Shape2 == invokingShape)
            {
                invokeCount++;
            }
        };
        vm.MoveShapes();

        // Assert
        invokeCount.Should().Be(2);
    }
}