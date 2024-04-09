using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels
{
    internal class PersonViewModel : BindableBase
    {
        private Person Person { get; set; } = new Person();
        public string Name => this.Person.Name;

        public string FirstName
        {
            get => this.Person.FirstName;
            set => this.SetAndRaise(value, this.Person, x => x.FirstName,
                propertyNames:nameof(this.Name));
        }

        public string LastName
        {
            get => this.Person.LastName;
            set => this.SetAndRaise(value, this.Person, x => x.LastName,
                propertyNames:nameof(this.Name));
        }
    }
}
