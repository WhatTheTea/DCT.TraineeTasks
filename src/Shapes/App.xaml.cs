﻿// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Resources;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.ViewModels;
using DCT.TraineeTasks.Shapes.Wrappers;
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
        Ioc.Default.ConfigureServices(this.Services);

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
            .AddKeyedSingleton<IFileService, JsonFileService>(".json")
            .AddKeyedSingleton<IFileService, BinaryFileService>(".bin")
            .AddKeyedSingleton<IFileService, XmlFileService>(".xml")
            .AddSingleton<MainViewModel>()
            .AddSingleton<LocalizerServiceObservableWrapper>()
            ;

        return services.BuildServiceProvider();
    }
}