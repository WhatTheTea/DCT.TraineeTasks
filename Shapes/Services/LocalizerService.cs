// <copyright file="LocalizerService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.Services;

public class LocalizerService
{
    private IStringLocalizer<LocalizerService> localizer;

    public LocalizerService(IStringLocalizer<LocalizerService> localizer)
    {
        this.localizer = localizer;
    }
    
    public string Circle => this.localizer["circle"];
    
    public string Square => this.localizer["square"];
    
    public string Triangle => this.localizer["triangle"];

    public string PlayButtonPlay => this.localizer["playButtonPlay"];

    public string PlayButtonPause => this.localizer["playButtonPause"];

    public string PlayButtonSelect => this.localizer["playButtonSelect"];
}