using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class LoginPageView : UserControl
{
    public LoginPageView()
    {
        this.InitializeComponent();
    }

    private void FormInput_OnLinkClick(object? sender, RoutedEventArgs e)
    {
        (this.DataContext as LoginPageViewModel)?.GoToRegisterPageCommand.Execute(null);
    }
}