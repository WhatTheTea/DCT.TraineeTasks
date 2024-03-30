// <copyright file="ShapeKindValueConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DCT.TraineeTasks.Shapes.Common;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;

public class ShapeKindValueConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is SupportedShapes shape)
        {
            return GeometryByShape(shape);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();

    private static Geometry GeometryByShape(SupportedShapes shape) =>
        shape switch
        {
            SupportedShapes.Circle => Views.Shapes.Circle,
            SupportedShapes.Square => Views.Shapes.Rectangle,
            SupportedShapes.Triangle => Views.Shapes.Triangle,
            _ => throw new ArgumentOutOfRangeException(nameof(shape))
        };
}
