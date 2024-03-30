using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.Shapes.Tests.ViewModels;

[SetUpFixture]
public class TestsWithConfiguredServices
{
    [OneTimeSetUp]
    public void OneTimeGlobalSetup()
    {
        ServiceCollection services = new();
        services
            .AddLogging()
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddSingleton<ILocalizationManager, LocalizationManager>(); // TODO: Stub
        ServiceProvider provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);
    }
}
