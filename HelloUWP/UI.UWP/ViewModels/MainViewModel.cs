// <copyright file = "MainViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class MainViewModel : BindableBase
{
    public ObservableCollection<PersonViewModel> People { get; } = [];

    public ICommand AddPersonCommand { get; set; }

    public MainViewModel()
    {
        this.AddPersonCommand = new RelayCommand(() =>
        {
            this.People.Add(new PersonViewModel()
            {
                FirstName = "Person",
                LastName = this.People.Count.ToString(),
            });
            this.People.LastOrDefault()?.Entries.Add(new EntryViewModel() { Text = "kek" });
        });
    }
}
