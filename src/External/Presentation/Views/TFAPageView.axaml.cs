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

public partial class TFAPageView : FormPage
{
    public TFAPageView()
    {
        InitializeComponent();
    }
    
    private void HandleLogoutButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as TFAPageViewModel)?.LogoutCommand.Execute(null);
    }
    
    private void HandleSendButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as TFAPageViewModel)?.SendCommand.Execute(null);
    }
}