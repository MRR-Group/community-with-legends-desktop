using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Domain.Entities;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class ReportsPageView : ScrollablePageView<ReportsPageViewModel, Report>
{
    public ReportsPageView()
    {
        InitializeComponent();
        InitScroll(ScrollHost);
    }
}