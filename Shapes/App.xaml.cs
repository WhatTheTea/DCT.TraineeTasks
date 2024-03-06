// <copyright file="App.xaml.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>


namespace DCT.TraineeTasks.Shapes;

using ReactiveUI;
using System.Reflection;
using Splat;


/// <summary>
///     Interaction logic for App.xaml.
/// </summary>
public partial class App
{
    public App()
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
    }
}