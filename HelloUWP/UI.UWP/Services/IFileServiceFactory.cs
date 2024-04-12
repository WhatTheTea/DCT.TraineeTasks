// <copyright file = "IFileServiceFactory.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

public interface IFileServiceFactory
{
    JsonFileService<T> GetJsonFileService<T>()
        where T : class;
}
