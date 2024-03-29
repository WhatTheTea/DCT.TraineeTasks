// <copyright file="PlayButtonTests.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels.MainViewModelTests;

[TestFixture]
[TestOf(typeof(MainViewModel))]
public class PlayButtonTests : Base
{
    private LocalizerService LocalizerService { get; } = Ioc.Default.GetService<LocalizerService>()
                                                         ?? throw new ArgumentNullException();

    public override void Setup()
    {
        base.Setup();
        var shape = new ShapeViewModel(0, 0, this.MonitoredViewModel.Subject.CanvasBoundary)
            { X = 10, Y = 10 };
        this.MonitoredViewModel.Subject.AddShape(shape);
        this.MonitoredViewModel.Subject.SelectedShape = shape;
    }

    [Test]
    public void PlayButtonPlayPause([Values(true, false)] bool isPaused)
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;

        // Act
        vm.SelectedShape!.IsPaused = isPaused;

        // Assert
        this.MonitoredViewModel.Should().RaisePropertyChangeFor(x => x.ButtonText);
        vm.ButtonText.Should()
            .Be(
                isPaused
                    ? this.LocalizerService.PlayButtonPlay
                    : this.LocalizerService.PlayButtonPause);
    }

    [Test]
    public void PlayButtonShapeNull()
    {
        // Arrange
        var vm = this.MonitoredViewModel.Subject;

        // Act
        vm.SelectedShape = null;

        // Assert
        this.MonitoredViewModel.Should().RaisePropertyChangeFor(x => x.ButtonText);
        vm.ButtonText.Should().Be(this.LocalizerService.PlayButtonSelect);
    }
}