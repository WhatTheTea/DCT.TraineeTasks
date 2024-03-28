// <copyright file="ShapeViewModelTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels;

[TestFixture]
[TestOf(typeof(ShapeViewModel))]
public class ShapeViewModelTests
{
    [SetUp]
    public void Setup()
    {
        this.TestedShapeViewModel = new ShapeViewModel(0, 0);
    }

    private ShapeViewModel TestedShapeViewModel { get; set; }

    [Test]
    public void Move()
    {
        // Arrange
        var vm = this.TestedShapeViewModel;
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
        var vm = this.TestedShapeViewModel;
        (vm.X, vm.Y) = (0, 0);
        vm.IsPaused = true;

        // Act
        vm.Move();

        // Assert
        (vm.X, vm.Y).Should().Be((0, 0));
    }
}