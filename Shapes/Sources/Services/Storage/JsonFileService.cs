// <copyright file="JsonFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Text.Json;
using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class JsonFileService : IFileService
{
    private static readonly JsonSerializerOptions s_options = new() { IncludeFields = true };

    public string FilePath { get; set; } = "movingShapes.json";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        ShapeDTO[] shapeArray = shapes.ToArray();
        string data = JsonSerializer.Serialize(shapeArray, s_options);
        File.WriteAllText(this.FilePath, data);
    }

    public IEnumerable<ShapeDTO> Load()
    {
        string text = File.ReadAllText(this.FilePath);
        IEnumerable<ShapeDTO> data = JsonSerializer.Deserialize<IEnumerable<ShapeDTO>>(text, s_options)
                                     ?? throw new FormatException("Invalid JSON");
        return data;
    }
}
