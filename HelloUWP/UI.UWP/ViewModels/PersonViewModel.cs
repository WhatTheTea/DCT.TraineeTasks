using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class PersonViewModel : BindableBase
{
    private Person _person;
    public string Name => this._person.Name;

    public ICommand DeleteCommand { get; set; }
    public ICommand DeleteEntryCommand { get; set; }
    public ICommand AddEntryCommand { get; set; }

    public string FirstName
    {
        get => this._person.FirstName;
        set => this.SetAndRaise(value, this._person, x => x.FirstName,
            propertyNames:nameof(this.Name));
    }

    public string LastName
    {
        get => this._person.LastName;
        set => this.SetAndRaise(value, this._person, x => x.LastName,
            propertyNames:nameof(this.Name));
    }

    public ObservableCollection<EntryViewModel> Entries { get; }

    public PersonViewModel()
    {
        this.Entries = [];
        this._person = new Person();

        this.DeleteEntryCommand = new RelayCommand<EntryViewModel>(x
            => this.Entries.Remove(x));
        this.AddEntryCommand = new RelayCommand(()
            => this.Entries.Add(new EntryViewModel() { Text = "Sample text"}));
    }

    public PersonViewModel(Person person)
    {
        this._person = person;
        this.Entries = new ObservableCollection<EntryViewModel>(person.Entries.Select(x => new EntryViewModel(x)));
    }
}
