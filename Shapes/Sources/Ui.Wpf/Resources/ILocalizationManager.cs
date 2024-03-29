// <copyright file="ILocalizationManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;

public interface ILocalizationManager : INotifyPropertyChanged, INotifyPropertyChanging
{
    /// <inheritdoc cref="LocalizationManager.uiCulture" />
    CultureInfo UiCulture { get; set; }

    /// <inheritdoc cref="LocalizationManager.culture" />
    CultureInfo Culture { get; set; }

    LocalizedString GetString(string name, params object[] args);
}