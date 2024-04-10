// <copyright file = "BlankPage1.xaml.cs" company = "Digital Cloud Technologies">
// Copyright (c) Digital Cloud Technologies.All rights reserved.
// </copyright>

using System;
using DCT.TraineeTasks.HelloUWP.UI.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DCT.TraineeTasks.HelloUWP.UI.UWP.Views.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class EditPage : Page
{
    public PersonViewModel ViewModel { get; set; }
   
    public EditPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        var viewModel = e.Parameter as PersonViewModel;
        this.ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        base.OnNavigatedTo(e);
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        this.Frame.Navigate(typeof(MainPage));
    }
}
