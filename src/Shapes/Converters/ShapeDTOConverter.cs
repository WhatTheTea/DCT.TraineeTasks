// <copyright file="ShapeDTOConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Primitives;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Converters;

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
        return new ShapeViewModel(dto.kind, dto.id)
        {
            IsPaused = dto.isPaused,
            X = dto.x,
            Y = dto.y,
            Velocity = new Point(dto.velocity)
        };
    }
}