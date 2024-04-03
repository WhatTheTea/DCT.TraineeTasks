// <copyright file = "ExtensionToSupportedConverter.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System.IO;
using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;

public static class ExtensionToSupportedConverter
{
    public static SupportedFileFormats Convert(string format) => Path.GetExtension(format) switch
    {
        ".bin" => SupportedFileFormats.Bin,
        ".json" => SupportedFileFormats.JSON,
        ".xml" => SupportedFileFormats.Xml,
        _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
    };
}
