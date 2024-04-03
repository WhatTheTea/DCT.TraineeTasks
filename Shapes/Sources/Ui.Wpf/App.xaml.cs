// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf;

/// <summary>
///     Interaction logic for App.xaml.
/// </summary>
public partial class App
{
    public App()
    {
        this.Services = ConfigureServices();
        Ioc.Default.ConfigureServices(this.Services);
        ConfigureSerilog();

        this.InitializeComponent();
    }

    public static new App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    private static ServiceProvider ConfigureServices()
    {
        ServiceCollection services = [];

        services
            .AddLogging(builder => builder.AddSerilog(dispose: true))
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddSingleton<ILocalizationManager, LocalizationManager>()
            .AddSingleton<IFileServiceFactory, FileServiceFactory>()
            .AddSingleton<MainViewModel>()
            ;

        return services.BuildServiceProvider();
    }

    private static void ConfigureSerilog() =>
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("shapes.log")
            .CreateLogger();
}
