// <copyright file="ShapeKindValueConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DCT.TraineeTasks.Shapes.Resources;

namespace DCT.TraineeTasks.Shapes.Converters;

public class ShapeKindValueConverter : IValueConverter
{
    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is SupportedShapes shape)
        {
            return this.GeometryByShape(shape);
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private Geometry GeometryByShape(SupportedShapes shape)
    {
        return shape switch
        {
            SupportedShapes.Circle => ShapesGeometries.Circle,
            SupportedShapes.Square => ShapesGeometries.Rectangle,
            SupportedShapes.Triangle => ShapesGeometries.Triangle,
            _ => throw new ArgumentOutOfRangeException(nameof(shape))
        };
    }
}