// <copyright file="ShapeDTOConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Common;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Converters;

public static class ShapeDTOConverter
{
    public static ShapeDTO ToDTO(this ShapeViewModel viewModel)
    {
        return new ShapeDTO(
            viewModel.Id,
            viewModel.X,
            viewModel.Y,
            viewModel.IsPaused,
            viewModel.Kind,
            (viewModel.Velocity.X, viewModel.Velocity.Y));
    }

    public static ShapeViewModel ToViewModel(this ShapeDTO dto)
    {
        return new ShapeViewModel(dto.Kind, dto.Id)
        {
            IsPaused = dto.IsPaused,
            X = dto.X,
            Y = dto.Y,
            Velocity = new Point(dto.Velocity)
        };
    }
}