using Avalonia;
using Avalonia.Controls;
using Presentation.ViewModels;

namespace Presentation.Views;

public class ViewBase : UserControl
{
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        
        var topLevel = TopLevel.GetTopLevel(this);
        
        if (topLevel is null) 
        {
            return;
        }

        if (DataContext is ViewModelBase viewModel)
        {
            viewModel.LoadNotificationForView(this);
        }
    }

}
