// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Views;

using System.Reactive.Disposables;
using ReactiveUI;

/// <summary>
///     Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    public MainWindow()
    {
        this.InitializeComponent();
        this.ViewModel = new MainWindowViewModel();
        this.WhenActivated(
            d =>
            {
                this.OneWayBind(
                        this.ViewModel,
                        vm => vm.Shapes,
                        v => v.ShapesListBox)
                    .DisposeWith(d);

                this.Bind(
                    this.ViewModel,
                    vm => vm.Boundaries.Height,
                    v => v.ShapesStackPanel.ActualHeight)
                    .DisposeWith(d);
                this.Bind(
                    this.ViewModel,
                    vm => vm.Boundaries.Width,
                    v => v.ShapesStackPanel.ActualWidth)
                    .DisposeWith(d);
            });
    }
}