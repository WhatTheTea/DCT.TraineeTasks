// <copyright file="LocalizerService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes;

public class LocalizerService(IStringLocalizer<LocalizerService> localizer)
{
    public string Circle => localizer["Circle"];

    public string Square => localizer["Square"];

    public string Triangle => localizer["Triangle"];

    public string PlayButtonPlay => localizer["playButtonPlay"];

    public string PlayButtonPause => localizer["playButtonPause"];

    public string PlayButtonSelect => localizer["playButtonSelect"];
}