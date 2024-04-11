// <copyright file = "IFileServiceFactory.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Services.Storage;

public interface IFileServiceFactory
{
    IFileService Create(SupportedFileFormats format, string path);
}
