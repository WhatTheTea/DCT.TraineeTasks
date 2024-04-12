// <copyright file = "MainViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.UI.UWP.Services;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;
using Microsoft.Extensions.DependencyInjection;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly IFileServiceFactory _fileServiceFactory
        = App.Services.GetService<IFileServiceFactory>()
          ?? throw new ArgumentNullException(nameof(IFileServiceFactory));

    private readonly IFileService<IEnumerable<Person>> _peopleFileService;

    private ObservableCollection<PersonViewModel> _people = [];

    public MainViewModel()
    {
        this._peopleFileService = this._fileServiceFactory.GetJsonFileService<IEnumerable<Person>>();
        this.AddPersonCommand = new RelayCommand(() => this.AddPerson(new PersonViewModel()));
        // TODO: AsyncCommands
        this.SaveStateCommand = new RelayCommand(async () => await this.SaveState());
        this.LoadStateCommand = new RelayCommand(async () => await this.LoadState());

        this.LoadStateCommand.Execute(null);
    }

    public ObservableCollection<PersonViewModel> People
    {
        get => this._people;
        private set => this.SetAndRaise(ref this._people, value);
    }

    public ICommand AddPersonCommand { get; }
    public ICommand SaveStateCommand { get; }
    public ICommand LoadStateCommand { get; }

    private async Task LoadState()
    {
        IEnumerable<Person>? models = await this._peopleFileService
            .LoadAsync(nameof(this.People));
        IEnumerable<PersonViewModel> viewModels = models.Select(x => new PersonViewModel(x));
        this.People.Clear();
        foreach (PersonViewModel person in viewModels)
        {
            this.AddPerson(person);
        }
    }

    private async Task SaveState() =>
        await this._peopleFileService.SaveAsync(
            this.People.Select(x => x.Model),
            nameof(this.People));

    private void AddPerson(PersonViewModel person)
    {
        person.DeleteCommand = new RelayCommand(() => this.People.Remove(person));
        this.People.Add(person);
    }
}
