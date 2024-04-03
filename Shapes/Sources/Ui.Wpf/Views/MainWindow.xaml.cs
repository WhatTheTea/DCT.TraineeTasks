// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using System.Windows.Threading;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;
using Microsoft.Win32;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Views;

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

        this.FrameTimer = new DispatcherTimer(
            TimeSpan.FromMilliseconds(21),
            DispatcherPriority.Render,
            (_, _) => this.ViewModel.MoveShapes(),
            App.Current.Dispatcher);

        this.ShapesCanvasItemsControl.LayoutUpdated += (_, _) =>
        {
            this.ViewModel.CanvasHeight = this.ShapesCanvasItemsControl.ActualHeight;
            this.ViewModel.CanvasWidth = this.ShapesCanvasItemsControl.ActualWidth;
        };
    }

    /// <summary>
    ///     Gets DispatcherTimer with frame time interval
    /// </summary>
    private DispatcherTimer FrameTimer { get; }

    public MainViewModel ViewModel => (MainViewModel)this.DataContext;

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dialog = new() { Filter = FormatFilter };

        if (dialog.ShowDialog() ?? false)
        {
            string fileName = dialog.FileName;
            this.ViewModel.SaveToCommand.Execute(fileName);
        }
    }

    private void LoadButton_OnClick(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new() { Filter = FormatFilter };

        if (dialog.ShowDialog() ?? false)
        {
            string fileName = dialog.FileName;
            this.ViewModel.LoadFromCommand.Execute(fileName);
        }
    }
}
