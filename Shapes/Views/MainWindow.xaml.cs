// <copyright file="MainWindow.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DCT.TraineeTasks.Shapes.ViewModels;
using ReactiveUI;
using Splat;

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
        this.ViewModel = Locator.Current.GetService<MainWindowViewModel>();
        this.Timer = new DispatcherTimer();
        this.Timer.Interval = TimeSpan.FromMilliseconds(21);

        IDisposable[] textBindings =
        [
            this.OneWayBind(
                this.ViewModel,
                vm => vm.MovingShapesNames,
                v => v.ShapesListBox.ItemsSource),
            this.OneWayBind(
                this.ViewModel,
                vm => vm.PlayButtonText,
                v => v.PlayButton.Content),
            this.OneWayBind(
                this.ViewModel,
                vm => vm.CircleText,
                v => v.CircleButton.Content),
            this.OneWayBind(
                this.ViewModel,
                vm => vm.SquareText,
                v => v.SquareButton.Content),
            this.OneWayBind(
                this.ViewModel,
                vm => vm.TriangleText,
                v => v.TriangleButton.Content)
        ];
        IDisposable[] shapesCommandBindings =
        [
            this.BindCommand(
                this.ViewModel,
                vm => vm.AddCircle,
                v => v.CircleButton),
            this.BindCommand(
                this.ViewModel,
                vm => vm.AddSquare,
                v => v.SquareButton),
            this.BindCommand(
                this.ViewModel,
                vm => vm.AddTriangle,
                v => v.TriangleButton)
        ];
        IDisposable[] commandBindings =
        [
            this.BindCommand(
                this.ViewModel,
                vm => vm.MoveShapes,
                v => v.Timer,
                nameof(this.Timer.Tick)),
            this.BindCommand(
                this.ViewModel,
                vm => vm.PlayPause,
                v => v.PlayButton),
            this.BindCommand(
                this.ViewModel,
                vm => vm.ChangeLanguage,
                v => v.UkrainianLanguageMenuItem,
                Observable.Return(CultureInfo.GetCultureInfo("uk"))),
            this.BindCommand(
                this.ViewModel,
                vm => vm.ChangeLanguage,
                v => v.EnglishLanguageMenuItem,
                Observable.Return(CultureInfo.GetCultureInfo("us"))),
            this.BindCommand(
                this.ViewModel,
                vm => vm.Save,
                v => v.SaveButton,
                Observable.Return(FileFormats.Bin)),
        ];
        this.WhenActivated(
            d =>
            {
                this.ViewModel!.Boundary = this.Boundary;
                _ = textBindings
                    .Union(commandBindings)
                    .Union(shapesCommandBindings)
                    .Select(x => x.DisposeWith(d));
                this.OneWayBind(
                        this.ViewModel,
                        vm => vm.MovingShapesViews,
                        v => v.ShapesCanvasItemsControl.ItemsSource)
                    .DisposeWith(d);
                this.Bind(
                        this.ViewModel,
                        vm => vm.Boundary,
                        v => v.Boundary,
                        this.ShapesCanvasItemsControl.Events().SizeChanged)
                    .DisposeWith(d);
                this.WhenAnyValue(x => x.ShapesListBox.SelectedItem)
                    .BindTo(this.ViewModel, x => x.SelectedShapeName)
                    .DisposeWith(d);
            });
        this.Timer.Start();
    }

    public DispatcherTimer Timer { get; set; }

    private Point Boundary
    {
        get => new(this.ShapesCanvasItemsControl.ActualWidth, this.ShapesCanvasItemsControl.ActualHeight);
        set => _ = value;
    }
}