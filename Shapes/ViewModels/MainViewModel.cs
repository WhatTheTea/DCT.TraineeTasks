// <copyright file="MainViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public sealed partial class MainViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<LocalizerService>>
{
    public ObservableCollection<object> MovingShapes { get; } = new();

    // [ObservableProperty] 
    public LocalizerService LocalizerService => App.Current.Services.GetService<LocalizerService>()
                                                ?? throw new ArgumentNullException(nameof(this.LocalizerService));

    [ObservableProperty]
    private string selectedShapeName = string.Empty;

    [ObservableProperty] private string circleButtonText;

    [ObservableProperty] private string triangleButtonText;

    [ObservableProperty] private string squareButtonText;
   
    public MainViewModel()
    {
        this.CircleButtonText = this.LocalizerService.Circle;
        this.SquareButtonText = this.LocalizerService.Square;
        this.TriangleButtonText = this.LocalizerService.Triangle;
        
        this.Messenger.RegisterAll(this);
    }

    [RelayCommand]
    private void AddShape(Geometry kind)
    {
        // TODO
    }

    private void MoveShapes()
    {
        // TODO
    }

    [RelayCommand]
    private void PlayOrPause(object shape)
    {
        // TODO
    }

    [RelayCommand]
    private void ChangeCulture(CultureInfo cultureInfo)
    {
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        this.Messenger.Send(new ValueChangedMessage<LocalizerService>(this.LocalizerService));
    }

    [RelayCommand]
    private void SaveTo(FileFormat format)
    {
        // TODO
    }

    public void Receive(ValueChangedMessage<LocalizerService> message)
    {
        var service = message.Value;
        this.CircleButtonText = service.Circle;
        this.SquareButtonText = service.Square;
        this.TriangleButtonText = service.Triangle;
    }
}