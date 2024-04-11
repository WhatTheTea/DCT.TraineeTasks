// <copyright file = "ILocalizationManager.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace DCT.TraineeTasks.Shapes.Ui.Wpf.Resources;

public interface ILocalizationManager : INotifyPropertyChanged, INotifyPropertyChanging
{
    CultureInfo UiCulture { get; set; }
    CultureInfo Culture { get; set; }
    LocalizedString GetString(string name, params object[] args);
}
