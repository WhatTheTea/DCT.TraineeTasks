// <copyright file="BinaryFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.IO;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;
using MessagePack;
using MessagePack.Resolvers;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Services.Storage;

public class BinaryFileService : IFileService
{
    public string FileLocation { get; set; } = "movingShapes.bin";

    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var dtos = shapes.ToArray()
            .Select(x => x.ToDTO());
        var bytes = MessagePackSerializer.Serialize(dtos, ContractlessStandardResolver.Options);
        using var file = File.Create(this.FileLocation);
        file.Write(bytes);
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        using var file = new FileStream(this.FileLocation, FileMode.Open);
        var shapes = MessagePackSerializer
            .Deserialize<ShapeDTO[]>(file, ContractlessStandardResolver.Options);
        return shapes.Select(x => x.ToViewModel());
    }
}