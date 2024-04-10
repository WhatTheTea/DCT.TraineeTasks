using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = App.Services.GetRequiredService<MainViewModel>();
                //?? throw new ArgumentNullException(nameof(MainViewModel));
        }

        private void PeopleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.FirstOrDefault() is null) return;
            object selected = e.AddedItems.First();
            this.Frame.Navigate(typeof(EditPage), selected as PersonViewModel);
        }
    }
}
