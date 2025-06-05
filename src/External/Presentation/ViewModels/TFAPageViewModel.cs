using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Flurl.Http;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public partial class TFAPageViewModel : AuthPageViewModel
{
    private LogOutInteractor _logOutInteractor;
    private ValidateTFAInteractor _validateTfaInteractor;
    
    public string Token { get; set; }
    
    
    public TFAPageViewModel(HistoryRouter<ViewModelBase> router, LogOutInteractor logOutInteractor, ValidateTFAInteractor validateTfaInteractor, PermissionRepository permissions): base(router)
    {
        _logOutInteractor = logOutInteractor;
        _validateTfaInteractor = validateTfaInteractor;
        Token = "";
    }

    [RelayCommand]
    private void Send()
    {
        SendForm(async () =>
        {
            try
            {
                await _validateTfaInteractor.Validate(Token);
                _router.GoTo<UsersPageViewModel>();
            }
            catch (FlurlHttpException e)
            {
                _exceptions.Add("token", "Invalid 2FA code.");
            }
        });
    }
    
    [RelayCommand]
    private void Logout()
    {
        SendForm(async () =>
        {
            await _logOutInteractor.LogOut();
            _router.GoTo<LoginPageViewModel>();
        });
    }
}