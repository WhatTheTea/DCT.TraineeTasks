// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.ViewModels;
using DCT.TraineeTasks.Shapes.ViewModels.Shapes;
using DCT.TraineeTasks.Shapes.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes;

/// <summary>
///     Interaction logic for App.xaml.
/// </summary>
public partial class App
{
    public App()
    {
        this.InitIoC();
    }

    private IServiceProvider Container { get; set; }

    private void InitIoC()
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureServices(
                services =>
                {
                    services.UseMicrosoftDependencyResolver();
                    var resolver = Locator.CurrentMutable;
                    resolver.InitializeSplat();
                    resolver.InitializeReactiveUI();

                    services.AddLogging()
                        .AddLocalization(options => options.ResourcesPath = "Resources")
                        .AddSingleton<LocalizerService>()
                        .AddTransient<ShapeViewFactory>()
                        .AddSingleton<IViewFor<MainWindowViewModel>, MainWindow>()
                        .AddSingleton<MainWindowViewModel>()
                        .AddTransient<CircleViewModel>()
                        .AddTransient<SquareViewModel>()
                        .AddTransient<TriangleViewModel>()
                        ;
                })
            .UseEnvironment(Environments.Development)
            .Build();
        this.Container = host.Services;
        this.Container.UseMicrosoftDependencyResolver();
    }
}