using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class RegisterPageView : UserControl
{
    public RegisterPageView()
    {
        this.InitializeComponent();
    }
    
    private void HandleLoginLinkClick(object? sender, RoutedEventArgs e)
    {
        (this.DataContext as RegisterPageViewModel)?.GoToLoginPageCommand.Execute(null);
    }

    private void HandleRegisterButtonClick(object? sender, RoutedEventArgs e)
    {
        (this.DataContext as RegisterPageViewModel)?.RegisterCommand.Execute(null);
    }
} 