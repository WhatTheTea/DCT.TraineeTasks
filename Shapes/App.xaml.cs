// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>


namespace DCT.TraineeTasks.Shapes;

using ReactiveUI;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;


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
            .ConfigureServices(services =>
            {
                services.UseMicrosoftDependencyResolver();
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();
                resolver.InitializeReactiveUI();

                services.AddLogging()
                    .AddLocalization(
                        options => options.ResourcesPath = "Resources")
                    ;
            })
            .UseEnvironment(Environments.Development)
            .Build();
        this.Container = host.Services;
        this.Container.UseMicrosoftDependencyResolver();
    }
}