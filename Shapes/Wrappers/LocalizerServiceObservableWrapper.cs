// <copyright file="LocalizerServiceObservableWrapper.cs" company="Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies. All rights reserved.
// </copyright>

using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using DCT.TraineeTasks.Shapes.Services;

namespace DCT.TraineeTasks.Shapes.Wrappers;

public class LocalizerServiceObservableWrapper(LocalizerService service) : ObservableObject
{
    private CultureInfo culture = CultureInfo.CurrentUICulture;
    
    public string PlayButtonSelect => service.PlayButtonSelect;

    public string PlayButtonPause => service.PlayButtonPause;

    public string PlayButtonPlay => service.PlayButtonPlay;

    public string Triangle => service.Triangle;

    public string Square => service.Square;

    public string Circle => service.Circle;

    public CultureInfo CurrentCulture
    {
        get => this.culture;
        set
        {
            Thread.CurrentThread.CurrentCulture = value;
            Thread.CurrentThread.CurrentUICulture = value;
            this.SetProperty(ref this.culture, value);
            this.OnPropertyChanged(nameof(this.Triangle));
            this.OnPropertyChanged(nameof(this.Circle));
            this.OnPropertyChanged(nameof(this.Square));
            this.OnPropertyChanged(nameof(this.PlayButtonSelect));
            this.OnPropertyChanged(nameof(this.PlayButtonPause));
            this.OnPropertyChanged(nameof(this.PlayButtonPlay));
            
        }
    }
}