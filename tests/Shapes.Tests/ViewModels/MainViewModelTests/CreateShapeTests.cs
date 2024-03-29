using System.Collections.ObjectModel;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

public class CreateShapeTests : Base
{
    [Test]
    public void ShapeExistsAndRaisesCollectionChanged()
    {
        // Arrange 
        var vm = this.MonitoredViewModel.Subject;
        var collection = vm.Shapes.Monitor();

        // Act
        vm.CreateShapeCommand.Execute(SupportedShapes.Triangle);

        // Assert
        collection.Should()
            .Raise(nameof(ObservableCollection<ShapeViewModel>.CollectionChanged));
        vm.Shapes.Should().NotBeEmpty();
    }
}