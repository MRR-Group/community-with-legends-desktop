using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _content;

    public MainViewModel(HistoryRouter<ViewModelBase> router,LogInInteractor logInInteractor): base(router)
    {
        Content = new LoginPageViewModel(router, logInInteractor);
        router.CurrentViewModelChanged += viewModel => Content = viewModel;
        router.GoTo<LoginPageViewModel>();
    }
}
