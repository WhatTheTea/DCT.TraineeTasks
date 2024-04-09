using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP
{
    public class BindableBase : INotifyPropertyChanged
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

        /// <summary>
        /// Using setter and raises OnPropertyChanged
        /// </summary>
        /// <param name="setter">Action to set a property</param>
        /// <param name="propertyName">Property to set</param>
        protected void SetAndRaise<TTarget, TValue>(
            TTarget target, TValue value, Expression<Func<TTarget, TValue>> selector,
            [CallerMemberName] string propertyName = null,
            params string[] propertyNames) 
        {
            var expression = (MemberExpression)selector.Body;
            var property = (PropertyInfo)expression.Member;
            property.SetValue(target, value);

            this.OnPropertyChanged(propertyName);

            this.NotifyOthers(propertyNames);
        }

        private void NotifyOthers(params string[] propertyNames)
        {
            if (propertyNames.Length < 1) return;

            foreach (string name in propertyNames)
            {
                this.OnPropertyChanged(name);
            }
        }
    }
}
