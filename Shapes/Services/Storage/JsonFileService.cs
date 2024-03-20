// <copyright file="JsonFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.IO;
using System.Text.Json;
using DCT.TraineeTasks.Shapes.Converters;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class JsonFileService : IFileService
{
    public string FileLocation { get; set; } = "movingShapes.json";

    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true
    };

    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var dtos = shapes.Select(x => x.ToDTO());
        var data = JsonSerializer.Serialize(dtos, Options);
        File.WriteAllText(this.FileLocation, data);
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        var text = File.ReadAllText(this.FileLocation);
        var data = JsonSerializer.Deserialize<IEnumerable<ShapeDTO>>(text, Options)
                   ?? throw new FileFormatException(this.FileLocation);
        return data.Select(x => x.ToViewModel());
    }
}