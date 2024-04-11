// <copyright file = "Person.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Models;

public class Person
{
    public string Name => $"{this.FirstName} {this.LastName}";
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public Entry[] Entries { get; set; } = Array.Empty<Entry>();
}
