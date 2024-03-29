using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.Exceptions;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.ShapeViewModelTests;

public class MovingTests : Base
{
    [Test]
    public void Move()
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;
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
        var vm = this.MonitoredViewModel.Subject;
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
        var vm = this.MonitoredViewModel.Subject;
        vm.Boundary = new Point();

        // Act
        var act = () => (vm.X, vm.Y) = (10, 10);

        // Assert
        act.Should().Throw<ShapeOutOfBoundsException>();
    }
}