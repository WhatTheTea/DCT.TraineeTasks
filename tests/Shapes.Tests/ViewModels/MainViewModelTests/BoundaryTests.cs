using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

public class BoundaryTests : Base
{
    [SetUp]
    public override void Setup()
    {
        base.Setup();
        this.MonitoredViewModel.Subject.AddShape(new ShapeViewModel(0, 0));
    }

    [Test]
    public void UpdatesForChildren()
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;

        // Act
        vm.CanvasHeight = 300;
        vm.CanvasWidth = 300;

        // Assert
        vm.Shapes[0]
            .Boundary
            .Should()
            .BeEquivalentTo(new Point(300, 300));
    }

    [Test]
    public void UpdatesBoundaryProperty()
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;

        // Act
        vm.CanvasHeight = 300;
        vm.CanvasWidth = 300;

        // Assert
        this.MonitoredViewModel
            .Should()
            .RaisePropertyChangeFor(x => x.CanvasBoundary);

        vm.CanvasBoundary
            .Should()
            .BeEquivalentTo(new Point(300, 300));
    }
}