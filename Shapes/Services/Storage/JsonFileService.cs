// <copyright file="JsonFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using DCT.TraineeTasks.Shapes.Converters;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class JsonFileService : IFileService
{
    private const string FilePath = "movingShapes.json";
    
    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var dtos = shapes.Select(x => x.ToDTO());
        var data = JsonSerializer.Serialize(dtos);
        File.WriteAllText(FilePath, data);
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        var text = File.ReadAllText(FilePath);
        var data = JsonSerializer.Deserialize<IEnumerable<ShapeDTO>>(text)
            ?? throw new FileFormatException(FilePath);
        return data.Select(x => x.ToViewModel());
    }
}