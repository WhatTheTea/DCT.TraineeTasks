// <copyright file = "XmlFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Xml;
using System.Xml.Serialization;
using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class XmlFileService : IFileService
{
    private XmlSerializer Serializer { get; } = new(
        typeof(ShapeDTO[]),
        [typeof(ShapeDTO), typeof((double x, double y))]);

    public string FilePath { get; set; } = "movingShapes.xml";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        ShapeDTO[] shapesArray = shapes.ToArray();
        using StreamWriter writer = new(this.FilePath);
        this.Serializer.Serialize(writer, shapesArray.ToArray());
    }

    public async Task SaveAsync(IEnumerable<ShapeDTO> shapes)
    {
        await using StringWriter writer = new();
        ShapeDTO[] shapesArray = shapes.ToArray();
        this.Serializer.Serialize(writer, shapesArray.ToArray());
        await File.WriteAllTextAsync(this.FilePath, writer.ToString()).ConfigureAwait(false);
    }

    public IEnumerable<ShapeDTO> Load()
    {
        using StreamReader reader = new(this.FilePath);
        using XmlReader xmlReader = XmlReader.Create(reader);
        ShapeDTO[] shapeArray = this.Serializer.Deserialize(xmlReader) as ShapeDTO[]
                                ?? throw new FormatException("Invalid XML");
        return shapeArray;
    }

    public async Task<ShapeDTO[]> LoadAsync()
    {
        using StringReader reader = new(await File.ReadAllTextAsync(this.FilePath).ConfigureAwait(false));
        using XmlReader xmlReader = XmlReader.Create(reader);
        ShapeDTO[] shapeArray = this.Serializer.Deserialize(xmlReader) as ShapeDTO[]
                                ?? throw new FormatException("Invalid XML");
        return shapeArray;
    }
}
