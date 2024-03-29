using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Services;
using DCT.TraineeTasks.Shapes.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels;

[SetUpFixture]
public class TestsWithConfiguredServices
{
    [OneTimeSetUp]
    public void OneTimeGlobalSetup()
    {
        var services = new ServiceCollection();
        services
            .AddLogging()
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddSingleton<LocalizerService>()
            .AddSingleton<LocalizerServiceObservableWrapper>();
        var provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);
    }
}