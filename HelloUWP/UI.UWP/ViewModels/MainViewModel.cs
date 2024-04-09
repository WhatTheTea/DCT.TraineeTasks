using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        public ObservableCollection<PersonViewModel> People { get; } = new ObservableCollection<PersonViewModel>();
    }
}
