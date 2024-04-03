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

    public async Task SaveAsync(IEnumerable<ShapeDTO> shapes)
    {
        ShapeDTO[] shapeArray = shapes.ToArray();
        await using FileStream file = File.Create(this.FilePath);
        await JsonSerializer.SerializeAsync(file, shapeArray, s_options)
            .ConfigureAwait(false);
    }

    public IEnumerable<ShapeDTO> Load()
    {
        string text = File.ReadAllText(this.FilePath);
        IEnumerable<ShapeDTO> data = JsonSerializer.Deserialize<IEnumerable<ShapeDTO>>(text, s_options)
                                     ?? throw new FormatException("Invalid JSON");
        return data;
    }

    public async Task<ShapeDTO[]> LoadAsync()
    {
        await using FileStream file = new(this.FilePath, FileMode.Open);
        return await JsonSerializer.DeserializeAsync<ShapeDTO[]>(file, s_options)
            .ConfigureAwait(false) ?? throw new FormatException("Invalid JSON");
    }
}
