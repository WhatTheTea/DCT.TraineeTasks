// <copyright file="KindToLocalizedStringConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Services;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;

public static class KindToLocalizedStringConverter
{
    private static LocalizerService service =
        Ioc.Default.GetService<LocalizerService>()
        ?? throw new ArgumentNullException(nameof(service));

    public static string ToLocalizedString(this SupportedShapes kind)
    {
        return kind switch
        {
            SupportedShapes.Circle => service.Circle,
            SupportedShapes.Square => service.Square,
            SupportedShapes.Triangle => service.Triangle,
            _ => throw new ArgumentException(null, nameof(kind))
        };
    }
}