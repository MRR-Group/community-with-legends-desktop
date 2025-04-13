using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Exceptions;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Presentation.Events;
using Presentation.Utils;

namespace Presentation.ViewModels;

public partial class LoginPageViewModel : AuthPageViewModel
{
    private LogInInteractor _logInInteractor;
    
    public string Email { get; set; }
    public string Password { get; set; }
    
    
    public LoginPageViewModel(HistoryRouter<ViewModelBase> router, LogInInteractor logInInteractor): base(router)
    {
        _logInInteractor = logInInteractor;

        Email = "";
        Password = "";
    }

    [RelayCommand]
    private void LogIn()
    {
        SendForm(async () => {
            try
            {
                await _logInInteractor.LogIn(Email, Password);
                _router.GoTo<AdminsPageViewModel>();
            }
            catch (FlurlHttpException e)
            {
                if (e.StatusCode == 403)
                {
                    _exceptions.Add("email", "Invalid username or password");
                    _exceptions.Add("password", "Invalid username or password");
                }
                else
                {
                    throw;
                }
            }
        });
    }
    
    [RelayCommand]
    private void GoToRegisterPage()
    {
        _router.GoTo<RegisterPageViewModel>();
    }
}