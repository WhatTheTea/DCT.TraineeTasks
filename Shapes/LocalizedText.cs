// <copyright file="LocalizedText.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace DCT.TraineeTasks.Shapes;

using Microsoft.Extensions.Localization;

public class LocalizedText(IStringLocalizer<LocalizedText> localizer)
{
    public string Circle => localizer.GetString("Circle");

    public string Square => localizer["Square"];

    public string Triangle => localizer["Triangle"];
}