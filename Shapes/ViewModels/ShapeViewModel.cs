// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public partial class ShapeViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<LocalizerService>>
{
    public int Id { get; }
    public SupportedShapes ShapeKind { get; }

    [ObservableProperty] private string name;
    [ObservableProperty] private double x;
    [ObservableProperty] private double y;

    public ShapeViewModel(int id, string name, SupportedShapes kind)
    {
        this.Id = id;
        this.ShapeKind = kind;
        this.name = name;
        this.Messenger.RegisterAll(this);
    }

    public void Receive(ValueChangedMessage<LocalizerService> message)
    {
        
    }

    [RelayCommand]
    private void Move()
    {
        
    }
}