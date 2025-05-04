using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _content;

    public MainViewModel(HistoryRouter<ViewModelBase> router,LogInInteractor logInInteractor, PermissionRepository permissions): base(router)
    {
//        Content = new LoginPageViewModel(router, logInInteractor, permissions);
        Content = new UsersPageViewModel(router);
        router.CurrentViewModelChanged += viewModel => Content = viewModel;
        router.GoTo<UsersPageViewModel>();
    }
}
