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
    private const string FilePath = "movingShapes.json";

    private static readonly JsonSerializerOptions options = new()
    {
        IncludeFields = true,
    };
    
    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var dtos = shapes.Select(x => x.ToDTO());
        var data = JsonSerializer.Serialize(dtos, options);
        File.WriteAllText(FilePath, data);
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        var text = File.ReadAllText(FilePath);
        var data = JsonSerializer.Deserialize<IEnumerable<ShapeDTO>>(text, options)
            ?? throw new FileFormatException(FilePath);
        return data.Select(x => x.ToViewModel());
    }
}