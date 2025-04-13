using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Presentation.ViewModels;
using Avalonia.VisualTree;
using Presentation.Controls;

namespace Presentation.Views;

public partial class RegisterPageView : UserControl
{
    public RegisterPageView()
    {
        this.InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (this.DataContext is RegisterPageViewModel viewModel)
        {
            viewModel.OnFormException += HandleFormException;
        }
    }

    private void HandleFormException(object? sender, RegisterPageViewModel.OnFormExceptionArguments e)
    {
        foreach (var input in this.GetVisualDescendants().OfType<FormInput>())
        {
            if (input.Name is null || !e.Exceptions.ContainsKey(input.Name.ToLower()))
            {
                input.Error = null;
                continue;
            }
            
            input.Error = e.Exceptions[input.Name.ToLower()];
        }
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