// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes;

/// <summary>
///     Interaction logic for App.xaml.
/// </summary>
public partial class App
{
    public App()
    {
        this.Services = ConfigureServices();

        this.InitializeComponent();
    }

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services
            .AddLogging()
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddSingleton<LocalizerService>()
            .AddSingleton<MainViewModel>()
            ;

        return services.BuildServiceProvider();
    }
}