using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Domain.Entities;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class AdminsPageView : ScrollablePageView<AdminsPageViewModel, Administrator>
{
    public AdminsPageView()
    {
        InitializeComponent();
        InitScroll(ScrollHost);
    }
    void CreateAdmin_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is AdminsPageViewModel model)
        {
            model.ShowCreateAdminDialogCommand?.Execute(null);
        }
    }
}