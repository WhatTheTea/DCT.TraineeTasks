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
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    public ObservableCollection<ShapeViewModel> Shapes { get; } = new();
    
    public LocalizerServiceObservableWrapper LocalizerService =>
        App.Current.Services.GetService<LocalizerServiceObservableWrapper>()
        ?? throw new ArgumentNullException(nameof(this.LocalizerService));

    [ObservableProperty]
    private string selectedShapeName = string.Empty;

    [RelayCommand]
    private void AddShape(SupportedShapes kind)
    {
        var shape = new ShapeViewModel(1337, "kek", kind);
        this.Shapes.Add(shape);
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
    private void ChangeCulture(CultureInfo cultureInfo) =>
        this.LocalizerService.CurrentCulture = cultureInfo;

    [RelayCommand]
    private void SaveTo(FileFormat format)
    {
        // TODO
    }
}