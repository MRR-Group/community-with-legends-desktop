using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Domain.Entities;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class LogsPageView  : ScrollablePageView<LogsPageViewModel, Log>
{
    public LogsPageView()
    {
        InitializeComponent();
        InitScroll(ScrollHost);
    }
}