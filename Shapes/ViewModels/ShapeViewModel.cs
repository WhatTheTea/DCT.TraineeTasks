// <copyright file="ShapeViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DCT.TraineeTasks.Shapes.Services;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public class ShapeViewModel : ObservableRecipient, IRecipient<ValueChangedMessage<LocalizerService>>
{
    public void Receive(ValueChangedMessage<LocalizerService> message)
    {
        
    }
}