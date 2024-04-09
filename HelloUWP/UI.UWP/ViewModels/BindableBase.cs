using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP
{
    internal class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) 
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaise<T>(ref T original, T value, [CallerMemberName] string propertyName = null) 
        {
            if (!original.Equals(value))
            {
                return;
            }

            original = value;

            this.OnPropertyChanged(propertyName);
        }
    }
}
