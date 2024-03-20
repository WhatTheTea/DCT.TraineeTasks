// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using DCT.TraineeTasks.Shapes.ViewModels;
using Microsoft.Win32;

namespace DCT.TraineeTasks.Shapes.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
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
            Filter = "JSON files (*.json)|*.json" 
            + "|Binary files (*.bin)|*.bin"
            + "|XML files (*.xml)|*.xml",
        };

        if (dialog.ShowDialog() ?? false)
        {
            Console.WriteLine(dialog.FileName);
        }
    }
}