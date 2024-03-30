// <copyright file="XmlFileService.cs" company="Digital Cloud Technologies">
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

    public string FileLocation { get; set; } = "movingShapes.xml";

    public void Save(IEnumerable<ShapeDTO> shapes)
    {
        ShapeDTO[] shapesArray = shapes.ToArray();
        using StreamWriter writer = new(this.FileLocation);
        this.Serializer.Serialize(writer, shapesArray.ToArray());
    }

    public IEnumerable<ShapeDTO> Load()
    {
        using StreamReader reader = new(this.FileLocation);
        using XmlReader xmlReader = XmlReader.Create(reader);
        ShapeDTO[] shapeArray = this.Serializer.Deserialize(xmlReader) as ShapeDTO[]
                                ?? throw new FormatException("Invalid XML");
        return shapeArray;
    }
}
