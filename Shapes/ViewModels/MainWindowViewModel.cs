// <copyright file="MainWindowViewModel.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>
namespace DCT.TraineeTasks.Shapes.ViewModels;

using System.Drawing;


using ReactiveUI;

public class MainWindowViewModel : ReactiveObject
{
    public IEnumerable<Models.Shape> Shapes { get; }

    public Rectangle Boundaries { get; set; } = new(0, 0, 0, 0);
}