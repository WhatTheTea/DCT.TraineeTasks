// <copyright file = "FileServiceFactory.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public class FileServiceFactory : IFileServiceFactory
{
    public IFileService Create(SupportedFileFormats format, string path) => format switch
    {
        SupportedFileFormats.Bin => new BinaryFileService { FilePath = path },
        SupportedFileFormats.JSON => new JsonFileService { FilePath = path },
        SupportedFileFormats.Xml => new XmlFileService { FilePath = path },
        _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
    };
}
