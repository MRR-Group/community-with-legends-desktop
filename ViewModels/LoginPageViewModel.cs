using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;

namespace CommunityWithLegends.ViewModels;

public partial class LoginPageViewModel(HistoryRouter<ViewModelBase> router) : ViewModelBase
{
    public string Test { get; set; } = "Login";

    private HistoryRouter<ViewModelBase> _router = router;

    [RelayCommand]
    private void GoToRegisterPage()
    {
        this._router.GoTo<RegisterPageViewModel>();
    }
}