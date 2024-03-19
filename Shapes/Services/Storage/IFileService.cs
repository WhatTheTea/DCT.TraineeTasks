// <copyright file="IFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.Immutable;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public interface IFileService
{
    void Save(IEnumerable<ShapeViewModel> shapes);

    IEnumerable<ShapeViewModel> Load();
}