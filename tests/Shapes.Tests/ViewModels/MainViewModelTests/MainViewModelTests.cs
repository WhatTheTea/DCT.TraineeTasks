// <copyright file="MainViewModelTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Events;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.ViewModels;
using FluentAssertions.Events;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public abstract class MainViewModelTests
{
    protected IMonitor<MainViewModel> MonitoredViewModel { get; set; }

    [SetUp]
    public virtual void Setup()
    {
        var vm = new MainViewModel
        {
            CanvasHeight = 100,
            CanvasWidth = 100,
        };
        this.MonitoredViewModel = vm.Monitor();
    }

    [TearDown]
    public virtual void TearDown()
    {
        this.MonitoredViewModel.Dispose();
    }
}