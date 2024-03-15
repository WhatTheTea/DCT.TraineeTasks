// <copyright file="LocalizerService.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Globalization;
using System.Reactive.Linq;
using Microsoft.Extensions.Localization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DCT.TraineeTasks.Shapes;

public class LocalizerService : ReactiveObject
{
    private CultureInfo currentCulture = CultureInfo.CurrentUICulture;
    private readonly IStringLocalizer<LocalizerService> localizer;

    public CultureInfo CurrentCulture
    {
        get => this.currentCulture;
        set
        {
            Thread.CurrentThread.CurrentUICulture = value;
            this.RaiseAndSetIfChanged(ref this.currentCulture, value);
        }
    }

    public LocalizerService(IStringLocalizer<LocalizerService> localizer)
    {
        this.localizer = localizer;

        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => this.localizer["Circle"].Value)
            .ToPropertyEx(this, x => x.Circle);

        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => this.localizer["Square"].Value)
            .ToPropertyEx(this, x => x.Square);
        
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => this.localizer["Triangle"].Value)
            .ToPropertyEx(this, x => x.Triangle);
        
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => this.localizer["playButtonSelect"].Value)
            .ToPropertyEx(this, x => x.PlayButtonSelect);
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => this.localizer["playButtonPause"].Value)
            .ToPropertyEx(this, x => x.PlayButtonPause);
        this.WhenAnyValue(x => x.CurrentCulture)
            .Select(_ => this.localizer["playButtonPlay"].Value)
            .ToPropertyEx(this, x => x.PlayButtonPlay);
    }

    [ObservableAsProperty]
    public string Circle { get; }
    
    [ObservableAsProperty]
    public string Square { get; }
    
    [ObservableAsProperty]
    public string Triangle { get; }

    [ObservableAsProperty]
    public string PlayButtonPlay { get; }

    [ObservableAsProperty]
    public string PlayButtonPause { get; }

    [ObservableAsProperty]
    public string PlayButtonSelect { get; }
}