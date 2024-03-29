// <copyright file="BinaryFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;
using MessagePack;
using MessagePack.Resolvers;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class BinaryFileService : IFileService
{
    public string FileLocation { get; set; } = "movingShapes.bin";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        var shapesArray = shapes.ToArray();
        var bytes = MessagePackSerializer.Serialize(shapesArray, ContractlessStandardResolver.Options);
        using var file = File.Create(this.FileLocation);
        file.Write(bytes);
    }

    public IEnumerable<ShapeDTO> Load()
    {
        using var file = new FileStream(this.FileLocation, FileMode.Open);
        var shapes = MessagePackSerializer
            .Deserialize<ShapeDTO[]>(file, ContractlessStandardResolver.Options);
        return shapes;
    }
}