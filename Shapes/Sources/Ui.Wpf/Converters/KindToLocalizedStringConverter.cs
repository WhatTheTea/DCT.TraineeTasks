// <copyright file="KindToLocalizedStringConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.DependencyInjection;
using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;

public static class KindToLocalizedStringConverter
{
    private static ILocalizationManager Localization { get; } =
        Ioc.Default.GetService<ILocalizationManager>()
        ?? throw new ArgumentNullException(nameof(Localization));

    public static string ToLocalizedString(this SupportedShapes kind) =>
        kind switch
        {
            SupportedShapes.Circle => Localization.GetString("circle"),
            SupportedShapes.Square => Localization.GetString("square"),
            SupportedShapes.Triangle => Localization.GetString("triangle"),
            _ => throw new ArgumentException(null, nameof(kind))
        };
}
