// <copyright file="MainViewModelTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Events;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class MainViewModelTests
{
    private MainViewModel ViewModel { get; set; }
    
    [SetUp]
    public void Setup()
    {
        this.ViewModel = new MainViewModel
        {
            CanvasHeight = 100,
            CanvasWidth = 100,
        };
    }
}