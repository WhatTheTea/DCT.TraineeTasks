// <copyright file="LocalizationManager.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;

public partial class LocalizationManager(IStringLocalizer<SharedResource> localizer)
    : ObservableObject, ILocalizationManager
{
    [ObservableProperty] private CultureInfo culture = CultureInfo.CurrentCulture;
    [ObservableProperty] private CultureInfo uiCulture = CultureInfo.CurrentUICulture;

    private IStringLocalizer<SharedResource> Localizer { get; } = localizer;

    public LocalizedString GetString(string name, params object[] args)
    {
        return this.Localizer.GetString(name, args);
    }

    partial void OnCultureChanging(CultureInfo value)
    {
        Thread.CurrentThread.CurrentCulture = value;
    }

    partial void OnUiCultureChanging(CultureInfo value)
    {
        Thread.CurrentThread.CurrentUICulture = value;
    }
}