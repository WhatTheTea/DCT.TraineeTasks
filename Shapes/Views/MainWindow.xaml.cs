// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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
                // // this.OneWayBind(
                // //         this.ViewModel,
                // //         vm => vm.ShapeNames,
                // //         v => v.ShapesListBox.Items)
                // //     .DisposeWith(d);
                // this.ShapesCanvas.Events().SizeChanged
                //     .Select(x => x.NewSize)
                //     .Subscribe(x =>
                //     {
                //         this.ViewModel.CanvasSize = x;
                //         var rect = new Rectangle
                //         {
                //             Height = x.Height / 10,
                //             Width = x.Width / 10,
                //             Fill = new SolidColorBrush(Colors.Black),
                //         };
                //         this.ShapesCanvas.Children.Add(rect);
                //         Canvas.SetTop(rect, 10);
                //         Canvas.SetLeft(rect, 10);
                //     })
                //     .DisposeWith(d);
            });
    }
}