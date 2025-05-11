using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Presentation.ViewModels;
using Avalonia.VisualTree;
using Presentation.Controls;

namespace Presentation.Views;

public partial class RegisterPageView : FormPage
{
    public RegisterPageView()
    {
        InitializeComponent();
    }
    
    private void HandleLoginLinkClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as RegisterPageViewModel)?.GoToLoginPageCommand.Execute(null);
    }

    private void HandleRegisterButtonClick(object? sender, RoutedEventArgs e)
    {
        (DataContext as RegisterPageViewModel)?.RegisterCommand.Execute(null);
    }
} 