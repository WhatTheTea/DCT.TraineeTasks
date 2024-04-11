// <copyright file = "IFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public interface IFileService
{
    string FilePath { get; set; }

    void Save(IEnumerable<ShapeDTO> shapes);
    Task SaveAsync(IEnumerable<ShapeDTO> shapes);

    IEnumerable<ShapeDTO> Load();
    Task<ShapeDTO[]> LoadAsync();
}
