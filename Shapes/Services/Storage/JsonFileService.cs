// <copyright file="JsonFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Text.Json;
using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class JsonFileService : IFileService
{
    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true
    };

    public string FileLocation { get; set; } = "movingShapes.json";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        var shapeArray = shapes.ToArray();
        var data = JsonSerializer.Serialize(shapeArray, Options);
        File.WriteAllText(this.FileLocation, data);
    }

    public IEnumerable<ShapeDTO> Load()
    {
        var text = File.ReadAllText(this.FileLocation);
        var data = JsonSerializer.Deserialize<IEnumerable<ShapeDTO>>(text, Options)
                   ?? throw new FormatException("Invalid JSON");
        return data;
    }
}