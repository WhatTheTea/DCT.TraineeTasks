// <copyright file="XmlFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class XmlFileService : IFileService
{
    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        throw new NotImplementedException();
    }
}