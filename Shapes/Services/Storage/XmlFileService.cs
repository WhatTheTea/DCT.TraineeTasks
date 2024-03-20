// <copyright file="XmlFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.IO;
using System.Xml.Serialization;
using DCT.TraineeTasks.Shapes.Converters;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class XmlFileService : IFileService
{
    public string FileLocation { get; set; } = "movingShapes.xml";

    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var dtos = shapes.Select(x => x.ToDTO());
        var serializer = new XmlSerializer(typeof(ShapeDTO[]), [typeof(ShapeDTO), typeof((double x, double y))]);
        using var writer = new StreamWriter(this.FileLocation);
        serializer.Serialize(writer, dtos.ToArray());
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        throw new NotImplementedException();
    }
}