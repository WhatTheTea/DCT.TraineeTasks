// <copyright file = "BinaryFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;
using MessagePack;
using MessagePack.Resolvers;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class BinaryFileService : IFileService
{
    public string FilePath { get; set; } = "movingShapes.bin";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        ShapeDTO[] shapesArray = shapes.ToArray();
        byte[] bytes = MessagePackSerializer.Serialize(shapesArray, ContractlessStandardResolver.Options);
        using FileStream file = File.Create(this.FilePath);
        file.Write(bytes);
    }

    public async Task SaveAsync(IEnumerable<ShapeDTO> shapes)
    {
        await using FileStream file = File.Create(this.FilePath);
        ShapeDTO[] shapesArray = shapes.ToArray();
        await MessagePackSerializer.SerializeAsync(file, shapesArray, ContractlessStandardResolver.Options)
            .ConfigureAwait(false);
    }

    public IEnumerable<ShapeDTO> Load()
    {
        using FileStream file = new(this.FilePath, FileMode.Open);
        ShapeDTO[] shapes = MessagePackSerializer
            .Deserialize<ShapeDTO[]>(file, ContractlessStandardResolver.Options);
        return shapes;
    }

    public async Task<ShapeDTO[]> LoadAsync()
    {
        await using FileStream file = new(this.FilePath, FileMode.Open);
        ValueTask<ShapeDTO[]> shapesTask = MessagePackSerializer
            .DeserializeAsync<ShapeDTO[]>(file, ContractlessStandardResolver.Options);
        return await shapesTask.AsTask().ConfigureAwait(false);
    }
}
