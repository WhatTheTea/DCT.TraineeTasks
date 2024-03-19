// <copyright file="BinaryFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Collections.Immutable;
using System.Collections.ObjectModel;
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
        var dtos = shapes.ToArray().Select(
            x =>
                new ShapeDTO(x.X, x.Y, x.IsPaused, x.ShapeKind, x.Id));
        var bytes = MessagePackSerializer.Serialize(dtos, ContractlessStandardResolver.Options);
        using var file = File.Create(FilePath);
        file.Write(bytes);
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        using var file = new FileStream(FilePath, FileMode.Open);
        var shapes = MessagePackSerializer
            .Deserialize<ShapeDTO[]>(file, ContractlessStandardResolver.Options);
        return shapes.Select(
            x => new ShapeViewModel(x.kind, x.id)
        {
            IsPaused = x.isPaused,
            X = x.x,
            Y = x.y,
        });
    }
}