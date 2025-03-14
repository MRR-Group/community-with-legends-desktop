using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;

namespace CommunityWithLegends.ViewModels;

public partial class RegisterPageViewModel(HistoryRouter<ViewModelBase> router) : ViewModelBase
{
    public string Test { get; set; } = "Register";

    private HistoryRouter<ViewModelBase> _router = router;

    [RelayCommand]
    private void GoToLoginPage()
    {
        this._router.GoTo<LoginPageViewModel>();
    }
}