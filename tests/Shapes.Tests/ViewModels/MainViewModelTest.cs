// <copyright file="MainViewModelTest.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Events;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.ViewModels;
using DCT.TraineeTasks.Shapes.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class MainViewModelTest
{
    private MainViewModel ViewModel { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new Mock<LocalizerServiceObservableWrapper>(
            new Mock<LocalizerService>(new Mock<IStringLocalizer<LocalizerService>>().Object).Object).Object);
        var provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);
    }
    
    [SetUp]
    public void Setup()
    {
        this.ViewModel = new MainViewModel();
        this.ViewModel.CanvasHeight = 100;
        this.ViewModel.CanvasWidth = 100;
    }
    
    // Intersections
    [Test]
    public void IntersectionSameShapes()
    {
        // Arrange
        using var vm = this.ViewModel.Monitor();
        var shape1 = new ShapeViewModel(0, 0) { X = 10, Y = 10, };
        var shape2 = new ShapeViewModel(0, 0) { X = 10, Y = 10, };
        vm.Subject.AddShape(shape1);
        vm.Subject.AddShape(shape2);

        // Act
        this.ViewModel.MoveShapes();

        // Assert
        vm.Should().Raise(nameof(MainViewModel.IntersectionOccured));
    }
}