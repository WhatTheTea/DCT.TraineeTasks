// <copyright file = "FileServiceFactory.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

public class FileServiceFactory : IFileServiceFactory
{
    public JsonFileService<T> GetJsonFileService<T>()
        where T : class
    {
        return new JsonFileService<T>();
    }
}
