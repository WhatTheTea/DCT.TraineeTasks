// <copyright file = "JsonFileService.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Services;

// TODO: make it disposable to prevent Open/Close spam
public class JsonFileService<T> : IFileService<T>
    where T : class
{
    private static readonly JsonSerializerOptions s_options = new() { IncludeFields = true };
    public async Task SaveAsync(T data, string path)
    {
        using FileStream file = File.Create(path);
        await JsonSerializer.SerializeAsync(file, data, s_options)
            .ConfigureAwait(false);
    }
    public async Task<T> LoadAsync(string path)
    {
        using FileStream file = new(path, FileMode.Open);
        return await JsonSerializer.DeserializeAsync<T>(file, s_options)
            .ConfigureAwait(false) ?? throw new FormatException("Invalid JSON");
    }
}
