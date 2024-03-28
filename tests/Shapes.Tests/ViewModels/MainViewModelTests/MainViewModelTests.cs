// <copyright file="MainViewModelTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.ViewModels;
using FluentAssertions.Events;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public abstract class MainViewModelTests
{
    [SetUp]
    public virtual void Setup()
    {
        var vm = new MainViewModel
        {
            CanvasHeight = 100,
            CanvasWidth = 100
        };
        this.MonitoredViewModel = vm.Monitor();
    }

    [TearDown]
    public virtual void TearDown()
    {
        this.MonitoredViewModel.Dispose();
    }

    protected IMonitor<MainViewModel> MonitoredViewModel { get; set; }
}