// <copyright file = "App.Configuration.xaml.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.ObjectModel;
using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP;

partial class App : Application
{
    private IServiceProvider? _serviceProvider;
    public static new App Current => (App)Application.Current;
    public static IServiceProvider Services
    {
        get =>
            Current._serviceProvider ?? throw new InvalidOperationException("Service provider is null");
    }

    private static IServiceProvider ConfigureServiceProvider()
    {
        IServiceCollection serviceCollection = new ServiceCollection()
            .AddSingleton<MainViewModel>()
            .AddSingleton<IFileServiceFactory, FileServiceFactory>();
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider;
    }
}
