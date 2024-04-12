// <copyright file = "Person.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Models;

public class Person(string firstName, string lastName)
{
    public Person(Person other) : this(other.FirstName, other.LastName) =>
        this.Entries = (Entry[])other.Entries.Clone();

    public Person() : this(string.Empty, string.Empty)
    {
    }

    public string Name => $"{this.FirstName} {this.LastName}";
    public string LastName { get; set; } = lastName;
    public string FirstName { get; set; } = firstName;
    public Entry[] Entries { get; set; } = [];
}
