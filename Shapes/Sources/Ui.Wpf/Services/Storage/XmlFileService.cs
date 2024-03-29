﻿// <copyright file="XmlFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.IO;
using System.Xml.Serialization;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Services.Storage;

public class XmlFileService : IFileService
{
    private readonly XmlSerializer serializer = new(
        typeof(ShapeDTO[]),
        [typeof(ShapeDTO), typeof((double x, double y))]);

    public string FileLocation { get; set; } = "movingShapes.xml";

    public void Save(IEnumerable<ShapeViewModel> shapes)
    {
        var dtos = shapes.Select(x => x.ToDTO());
        using var writer = new StreamWriter(this.FileLocation);
        this.serializer.Serialize(writer, dtos.ToArray());
    }

    public IEnumerable<ShapeViewModel> Load()
    {
        using var reader = new StreamReader(this.FileLocation);
        var dtos = this.serializer.Deserialize(reader) as ShapeDTO[]
                   ?? throw new FileFormatException(new Uri(this.FileLocation));
        return dtos.Select(x => x.ToViewModel());
    }
}