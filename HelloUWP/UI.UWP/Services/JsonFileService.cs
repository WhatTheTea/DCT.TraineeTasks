// <copyright file = "JsonFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

// TODO: make it disposable to prevent Open/Close spam
public class JsonFileService<T> : IFileService<T>
    where T : class
{
    private static readonly JsonSerializerOptions s_options = new() { IncludeFields = true };
    public async Task SaveAsync(T data, string path)
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        StorageFile storageFile = await storageFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
        using Stream? file = await storageFile.OpenStreamForWriteAsync();
        await JsonSerializer.SerializeAsync(file, data, s_options);
    }
    public async Task<T> LoadAsync(string path)
    {
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        StorageFile storageFile = await storageFolder.GetFileAsync(path);
        using Stream? file = await storageFile.OpenStreamForReadAsync();
        return await JsonSerializer.DeserializeAsync<T>(file, s_options)
               ?? throw new FormatException("Invalid JSON");
    }
}
