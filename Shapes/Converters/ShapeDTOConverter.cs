// <copyright file="ShapeDTOConverter.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Primitives;
using DCT.TraineeTasks.Shapes.Services.Storage;
using DCT.TraineeTasks.Shapes.ViewModels;

namespace DCT.TraineeTasks.Shapes.Converters;

public static class ShapeDTOConverter
{
    public static ShapeDTO ToDTO(this ShapeViewModel viewModel) =>
        new(
            viewModel.Id,
            viewModel.X,
            viewModel.Y,
            viewModel.IsPaused,
            viewModel.ShapeKind,
            (viewModel.Velocity.X, viewModel.Velocity.Y));

    public static ShapeViewModel ToViewModel(this ShapeDTO dto) =>
        new(dto.kind, dto.id)
        {
            IsPaused = dto.isPaused,
            X = dto.x,
            Y = dto.y,
            Velocity = new Point(dto.velocity),
        };
}