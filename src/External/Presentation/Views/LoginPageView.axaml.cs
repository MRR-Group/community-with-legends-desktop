using System;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Presentation.Controls;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class LoginPageView : FormPage
{
    public LoginPageView()
    {
        InitializeComponent();
    }
    
    private void HandleRegisterButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as LoginPageViewModel)?.GoToRegisterPageCommand.Execute(null);
    }
    
    private void HandleLoginButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as LoginPageViewModel)?.LogInCommand.Execute(null);
    }
}