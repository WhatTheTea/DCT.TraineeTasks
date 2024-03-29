// <copyright file="IFileService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using DCT.TraineeTasks.Shapes.Ui.Wpf.ViewModels;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Services.Storage;

public interface IFileService
{
    string FileLocation { get; set; }

    void Save(IEnumerable<ShapeViewModel> shapes);

    IEnumerable<ShapeViewModel> Load();
}