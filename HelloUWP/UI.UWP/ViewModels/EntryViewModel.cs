// <copyright file = "EntryViewModel.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using DCT.TraineeTasks.HelloUWP.UI.UWP.Models;
using DCT.TraineeTasks.HelloUWP.WhatTheToolkit;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;

public class EntryViewModel(Entry entry) : BindableBase
{
    public EntryViewModel() : this(new Entry())
    {
    }

    public string Text
    {
        get => entry.Text;
        set => this.SetAndRaise(value, entry, x => x.Text);
    }
}
