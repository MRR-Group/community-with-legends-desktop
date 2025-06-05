using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Flurl.Http;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Presentation.Events;
using Presentation.Utils;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class LoginPageViewModel : AuthPageViewModel
{
    private LogInInteractor _logInInteractor;
    private ValidateTFAInteractor _validateTFAInteractor;
    private PermissionRepository _permissions;
    private TFAService _tfa;

    public string Email { get; set; }
    public string Password { get; set; }
    
    
    public LoginPageViewModel(HistoryRouter<ViewModelBase> router, LogInInteractor logInInteractor, PermissionRepository permissions, TFAService tfa): base(router)
    {
        _logInInteractor = logInInteractor;
        _permissions = permissions;
        _tfa = tfa;
        Email = "";
        Password = "";
    }

    [RelayCommand]
    private void LogIn()
    {
        SendForm(async () => {
            try
            {
                var user = await _logInInteractor.LogIn(Email, Password);
                _permissions.Load(user.Permissions);
                
                if (_permissions.Cannot(Permission.ViewUsers))
                {
                    ShowNotification("Log in failed", "You need at least moderator role to login.");
                    return;
                }
                
                var tfaStatus = await _tfa.Check();
                
                if (!tfaStatus)
                {
                    _router.GoTo<TFAPageViewModel>();
                    return;
                }

                ShowNotification("Logged in!", $"Welcome {user.Name}!");
                _router.GoTo<UsersPageViewModel>();
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