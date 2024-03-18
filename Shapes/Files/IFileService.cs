// <copyright file="IFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.Immutable;
using DCT.TraineeTasks.Shapes.ViewModels;
using DCT.TraineeTasks.Shapes.Views;

namespace DCT.TraineeTasks.Shapes.Models.MovingShapes;

public interface IFileService
{
    void Save(IEnumerable<ShapeViewModel> shapes);

    ImmutableArray<ShapeViewModel> Load();
}