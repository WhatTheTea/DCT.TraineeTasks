// <copyright file="XmlFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Xml.Serialization;
using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class XmlFileService : IFileService
{
    private readonly XmlSerializer serializer = new(
        typeof(ShapeDTO[]),
        [typeof(ShapeDTO), typeof((double x, double y))]);

    public string FileLocation { get; set; } = "movingShapes.xml";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        var shapesArray = shapes.ToArray();
        using var writer = new StreamWriter(this.FileLocation);
        serializer.Serialize(writer, shapesArray.ToArray());
    }

    public IEnumerable<ShapeDTO> Load()
    {
        using var reader = new StreamReader(this.FileLocation);
        var shapeArray = serializer.Deserialize(reader) as ShapeDTO[]
                         ?? throw new FormatException("Invalid XML");
        return shapeArray;
    }
}