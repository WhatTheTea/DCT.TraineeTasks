using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels
{
    public class PersonViewModel : BindableBase
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

        public Entry[] Entries
        {
            get => this.Person.Entries;
            set => this.SetAndRaise(value, this.Person, x => x.Entries);
        }
    }
}
