using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Exceptions;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using Presentation.Utils;

namespace Presentation.ViewModels;

public partial class RegisterPageViewModel : AuthPageViewModel
{
    private RegisterInteractor _registerInteractor;
    
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
    public RegisterPageViewModel(HistoryRouter<ViewModelBase> router, RegisterInteractor registerInteractor) : base(router)
    {
        _registerInteractor = registerInteractor;

        Email = "";
        Name = "";
        Password = "";
        ConfirmPassword = "";
    }

    [RelayCommand]
    private void Register()
    {
        SendForm(async () => {
            await _registerInteractor.Register(Name, Email, Password, ConfirmPassword);
            _router.GoTo<LoginPageViewModel>();
        });
    }
    
    [RelayCommand]
    private void GoToLoginPage()
    {
        _router.GoTo<LoginPageViewModel>();
    }
}