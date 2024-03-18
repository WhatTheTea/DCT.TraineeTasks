// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DCT.TraineeTasks.Shapes.Services;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public partial class ShapeViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<LocalizerService>>
{
    public int Id { get; }
    [ObservableProperty] private string name;
    [ObservableProperty] private double x;
    [ObservableProperty] private double y;
    [ObservableProperty] private Geometry geometry;

    public ShapeViewModel(int id, string name, Geometry geometry)
    {
        this.Id = id;
        this.name = name;
        this.geometry = geometry;
        this.Messenger.RegisterAll(this);
    }

    public void Receive(ValueChangedMessage<LocalizerService> message)
    {
        
    }
}