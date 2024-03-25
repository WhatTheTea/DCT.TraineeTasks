﻿// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace DCT.TraineeTasks.Shapes.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    private const string FormatFilter = "JSON files (*.json)|*.json"
                                        + "|Binary files (*.bin)|*.bin"
                                        + "|XML files (*.xml)|*.xml";

    /// <summary>
    ///     Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();

        this.ShapesCanvasItemsControl.LayoutUpdated += (_, _) =>
        {
            this.ViewModel.CanvasHeight = this.ShapesCanvasItemsControl.ActualHeight;
            this.ViewModel.CanvasWidth = this.ShapesCanvasItemsControl.ActualWidth;
        };
    }

    public MainViewModel ViewModel => (MainViewModel)this.DataContext;

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Filter = FormatFilter,
        };

        if (dialog.ShowDialog() ?? false)
        {
            var extension = Path.GetExtension(dialog.FileName);
            var service = GetService(extension.ToLower());
            service.FileLocation = dialog.FileName;
            this.ViewModel.SaveToCommand.Execute(service);
        }
    }

    private void LoadButton_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = FormatFilter,
        };

        if (dialog.ShowDialog() ?? false)
        {
            var extension = Path.GetExtension(dialog.FileName);
            var service = GetService(extension.ToLower());
            service.FileLocation = dialog.FileName;
            this.ViewModel.LoadFromCommand.Execute(service);
        }
    }

    private static IFileService GetService(string format) => Ioc.Default.GetKeyedService<IFileService>(format)
                                                             ?? throw new ArgumentOutOfRangeException(nameof(format));

}