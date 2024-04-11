// <copyright file = "CreateShapeTests.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;
using FluentAssertions.Events;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

public class CreateShapeTests : Base
{
    [Test]
    public void ShapeExistsAndRaisesCollectionChanged()
    {
        // Arrange 
        MainViewModel? vm = this.MonitoredViewModel.Subject;
        IMonitor<ObservableCollection<ShapeViewModel>>? collection = vm.Shapes.Monitor();

        // Act
        vm.CreateShapeCommand.Execute(SupportedShapes.Triangle);

        // Assert
        collection.Should()
            .Raise(nameof(ObservableCollection<ShapeViewModel>.CollectionChanged));
        vm.Shapes.Should().NotBeEmpty();
    }

    [Test]
    public void ShapeIdIsCalculated()
    {
        // Arrange 
        MainViewModel? vm = this.MonitoredViewModel.Subject;

        // Act
        vm.CreateShapeCommand.Execute(0);
        vm.CreateShapeCommand.Execute(0);
        vm.CreateShapeCommand.Execute(SupportedShapes.Triangle);

        // Assert
        vm.Shapes[1].Id.Should().Be(1);

        // Triangle is different kind, id calculated separately
        vm.Shapes[2].Id.Should().Be(0);
    }
}
