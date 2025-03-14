using Avalonia.Controls;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityWithLegends.Views;

namespace CommunityWithLegends.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _content;

    public MainViewModel(HistoryRouter<ViewModelBase> router)
    {
        this.Content = new LoginPageViewModel(router);
        router.CurrentViewModelChanged += viewModel => this.Content = viewModel;
        router.GoTo<LoginPageViewModel>();
    }
}
