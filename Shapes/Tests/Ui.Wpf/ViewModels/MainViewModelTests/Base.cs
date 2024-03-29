// <copyright file="Base.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;
using FluentAssertions.Events;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public abstract class Base
{
    [SetUp]
    public virtual void Setup()
    {
        var vm = new MainViewModel
        {
            CanvasHeight = 100,
            CanvasWidth = 100,
            CanvasBoundary = { X = 100, Y = 100 },
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