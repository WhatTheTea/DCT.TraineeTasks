// <copyright file="IFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.Immutable;

namespace DCT.TraineeTasks.Shapes.Models.MovingShapes;

public interface IFileService
{
    void Save(IEnumerable<MovingShape> shapes);

    ImmutableArray<MovingShape> Load();
}