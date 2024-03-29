using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

public class BoundaryTests : Base
{
    [Test]
    public void UpdatesForChildren()
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;
        vm.AddShape(new ShapeViewModel(0, 0));

        // Act
        vm.CanvasHeight = 300;
        vm.CanvasWidth = 300;

        // Assert
        vm.Shapes[0]
            .Boundary
            .Should()
            .BeEquivalentTo(new Point(300, 300));
    }
}