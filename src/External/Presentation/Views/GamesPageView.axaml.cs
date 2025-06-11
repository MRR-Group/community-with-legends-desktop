using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Domain.Entities;
using Presentation.ViewModels;

namespace Presentation.Views;

public partial class GamesPageView : ScrollablePageView<GamesPageViewModel, Game>
{
    public GamesPageView()
    {
        InitializeComponent();
        InitScroll(ScrollHost);
    }
}