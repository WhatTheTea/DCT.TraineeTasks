// <copyright file="ShapeViewModelTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.ViewModels;
using DCT.TraineeTasks.Shapes.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Tests.Shapes.ViewModels;

[TestFixture]
[TestOf(typeof(ShapeViewModel))]
public class ShapeViewModelTests
{
    private ShapeViewModel TestedShapeViewModel { get; set; }

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
        this.TestedShapeViewModel = new ShapeViewModel(0, 0, new Point(20, 20));
    }

    [Test]
    public void Move()
    {
        var vm = this.TestedShapeViewModel;
        (vm.X, vm.Y) = (0, 0);
        vm.Move();
        (vm.X, vm.Y).Should().Be((10, 10));
    }

    [Test]
    public void DontMoveWhenPaused()
    {
        var vm = this.TestedShapeViewModel;
        (vm.X, vm.Y) = (0, 0);
        vm.IsPaused = true;
        vm.Move();
        (vm.X, vm.Y).Should().Be((0, 0));
    }
}