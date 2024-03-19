// <copyright file="BinaryFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.Immutable;
using System.IO;
using DCT.TraineeTasks.Shapes.ViewModels;
using MessagePack;
using MessagePack.Resolvers;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class BinaryFileService : IFileService
{
    private const string FilePath = "movingShapes.bin";

    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var bytes = MessagePackSerializer.Serialize(shapes, ContractlessStandardResolver.Options);
        using var file = File.Create(FilePath);
        file.Write(bytes);
    }

    public ImmutableArray<ShapeViewModel> Load()
    {
        using var file = new FileStream(FilePath, FileMode.Open);
        var shapes = MessagePackSerializer
            .Deserialize<ImmutableArray<ShapeViewModel>>(file);
        return shapes;
    }
}