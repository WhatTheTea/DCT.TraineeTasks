﻿// <copyright file = "PlayButtonTests.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class PlayButtonTests : Base
{
    private ILocalizationManager Localization { get; } = Ioc.Default.GetService<ILocalizationManager>()
                                                         ?? throw new ArgumentNullException();

    public override void Setup()
    {
        base.Setup();
        ShapeViewModel shape = new(0, 0, this.MonitoredViewModel.Subject.CanvasBoundary) { X = 10, Y = 10 };
        this.MonitoredViewModel.Subject.AddShape(shape);
        this.MonitoredViewModel.Subject.SelectedShape = shape;
    }

    [Test]
    public void PlayButtonPlayPause([Values(true, false)] bool isPaused)
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;

        // Act
        vm.SelectedShape!.IsPaused = isPaused;

        // Assert
        this.MonitoredViewModel.Should().RaisePropertyChangeFor(x => x.PlayButtonText);
        vm.PlayButtonText.Should()
            .Be(
                isPaused
                    ? this.Localization.GetString("playButtonPlay")
                    : this.Localization.GetString("playButtonPause"));
    }

    [Test]
    public void PlayButtonShapeNull()
    {
        // Arrange
        MainViewModel? vm = this.MonitoredViewModel.Subject;

        // Act
        vm.SelectedShape = null;

        // Assert
        this.MonitoredViewModel.Should().RaisePropertyChangeFor(x => x.PlayButtonText);
        vm.PlayButtonText.Should().Be(this.Localization.GetString("playButtonSelect"));
    }
}
