// <copyright file="LocalizedText.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes;

public class LocalizedText(IStringLocalizer<LocalizedText> localizer)
{
    public string Circle => localizer["Circle"];

    public string Square => localizer["Square"];

    public string Triangle => localizer["Triangle"];
}