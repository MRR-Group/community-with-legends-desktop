using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Services;

namespace Presentation.ViewModels;

public partial class RegisterPageViewModel : ViewModelBase
{
    private HistoryRouter<ViewModelBase> router;
    private SignUpInteractor signUpInteractor;
    
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
    public bool Processing { get; set; }
        
    public RegisterPageViewModel(HistoryRouter<ViewModelBase> router, SignUpInteractor signUpInteractor)
    {
        this.router = router;
        this.signUpInteractor = signUpInteractor;
    }

    [RelayCommand]
    private void Register()
    {
        TryRegister();
    }

    private async Task TryRegister()
    {
        Processing = true;
        await signUpInteractor.SignUp(Name, Email, Password, ConfirmPassword);
        Processing = false;
        
        router.GoTo<LoginPageViewModel>();
    }

    [RelayCommand]
    private void GoToLoginPage()
    {
        router.GoTo<LoginPageViewModel>();
    }
}