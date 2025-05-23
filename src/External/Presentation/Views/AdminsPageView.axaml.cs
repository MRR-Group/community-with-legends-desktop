using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class AdminsPageView : ViewBase
{
    public AdminsPageView()
    {
        this.InitializeComponent();
    }
    void CreateAdmin_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AdminsPageViewModel model)
        {
            model.ShowCreateAdminDialogCommand?.Execute(null);
        }
    }
}