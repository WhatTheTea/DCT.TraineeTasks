// <copyright file="MovingTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Exceptions;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.ShapeViewModelTests;

public class MovingTests : Base
{
    [Test]
    public void Move()
    {
        // Arrange
        ShapeViewModel? vm = this.MonitoredViewModel.Subject;
        (vm.X, vm.Y) = (0, 0);
        vm.Boundary = new Point(50, 50);

        // Act
        vm.Move();

        // Assert
        (vm.X, vm.Y).Should().Be((10, 10));
    }

    [Test]
    public void DontMoveWhenPaused()
    {
        // Arrange
        ShapeViewModel? vm = this.MonitoredViewModel.Subject;
        (vm.X, vm.Y) = (0, 0);

        // Act
        vm.IsPaused = true;
        vm.Move();

        // Assert
        (vm.X, vm.Y).Should().Be((0, 0));
    }

    [Test]
    public void ThrowsExceptionOutOfBounds()
    {
        // Arrange
        ShapeViewModel? vm = this.MonitoredViewModel.Subject;
        vm.Boundary = new Point();

        // Act
        Func<(double X, double Y)> act = () => (vm.X, vm.Y) = (10, 10);

        // Assert
        act.Should().Throw<ShapeOutOfBoundsException>();
    }
}
