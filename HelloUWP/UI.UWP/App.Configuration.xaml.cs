// <copyright file = "App.Configuration.xaml.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP
{
    partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        public static new App Current => (App)Application.Current;
        public static IServiceProvider Services
        {
            get =>
                Current._serviceProvider ?? throw new InvalidOperationException("Service provider is null");
        }

        private static IServiceProvider ConfigureServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection()
                .AddTransient<MainViewModel>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
