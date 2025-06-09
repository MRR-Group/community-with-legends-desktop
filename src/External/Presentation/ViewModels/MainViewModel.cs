using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _content;

    public MainViewModel(HistoryRouter<ViewModelBase> router, LogInInteractor logInInteractor, PermissionRepository permissions, TFAService tfa): base(router)
    {
        Content = new LoginPageViewModel(router, logInInteractor, permissions, tfa);
        
        router.CurrentViewModelChanged += viewModel => Content = viewModel;
        router.GoTo<LoginPageViewModel>();
    }
}
