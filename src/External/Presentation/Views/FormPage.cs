using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Presentation.Controls;
using Presentation.Events;
using Presentation.ViewModels;

namespace Presentation.Views;

public class FormPage : UserControl
{
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        if (DataContext is FormPageViewModel viewModel)
        {
            viewModel.OnFormException += HandleFormException;
        }
    }
    
    private void HandleFormException(object? sender, FormExceptionEventArgs e)
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
}