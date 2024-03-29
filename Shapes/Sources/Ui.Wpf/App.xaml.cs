// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Wrappers;
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

    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services
            .AddLogging(builder => builder.AddSerilog(dispose: true))
            .AddLocalization(options => options.ResourcesPath = @"Resources")
            .AddSingleton<LocalizerService>()
            .AddKeyedSingleton<IFileService, JsonFileService>(".json")
            .AddKeyedSingleton<IFileService, BinaryFileService>(".bin")
            .AddKeyedSingleton<IFileService, XmlFileService>(".xml")
            .AddSingleton<MainViewModel>()
            .AddSingleton<LocalizerServiceObservableWrapper>()
            ;

        return services.BuildServiceProvider();
    }

    private static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("shapes.log")
            .CreateLogger();
    }
}