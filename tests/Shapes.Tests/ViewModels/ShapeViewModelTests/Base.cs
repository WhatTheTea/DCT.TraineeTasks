// <copyright file="Base.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.ViewModels;
using FluentAssertions.Events;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.ShapeViewModelTests;

[TestFixture]
[TestOf(typeof(ShapeViewModel))]
public abstract class Base
{
    [SetUp]
    public virtual void Setup()
    {
        var vm = new ShapeViewModel(0, 0);
        this.MonitoredViewModel = vm.Monitor();
    }

    [TearDown]
    public virtual void TearDown()
    {
        this.MonitoredViewModel.Dispose();
    }

    protected IMonitor<ShapeViewModel> MonitoredViewModel { get; set; } = null!;
}