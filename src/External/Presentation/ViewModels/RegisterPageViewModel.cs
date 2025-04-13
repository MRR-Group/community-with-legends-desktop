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

public partial class RegisterPageViewModel : ViewModelBase
{
    private HistoryRouter<ViewModelBase> router;
    private RegisterInteractor _registerInteractor;
    
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    [ObservableProperty]
    private bool processing;
    
    private FormExceptions formExceptions = new ();

    public event EventHandler<OnFormExceptionArguments> OnFormException;

    public class OnFormExceptionArguments : EventArgs
    {
        public Dictionary<string, string> Exceptions;
    }

    public RegisterPageViewModel(HistoryRouter<ViewModelBase> router, RegisterInteractor registerInteractor)
    {
        this.router = router;
        this._registerInteractor = registerInteractor;
        this.Processing = false;

        this.Email = "";
        this.Name = "";
        this.Password = "";
        this.ConfirmPassword = "";
    }

    [RelayCommand]
    private void Register()
    {
        TryRegister();
    }

    private async Task TryRegister()
    {
        try
        {
            Processing = true;
            formExceptions.Clear();
            
            await _registerInteractor.Register(Name, Email, Password, ConfirmPassword);
            
            router.GoTo<LoginPageViewModel>();
        }
        catch (PasswordsDoNotMatchException e)
        {
            formExceptions.Add("password", e.Message);
        }
        catch (InvalidPasswordException e)
        {
            formExceptions.Add("password", e.Message);
        }
        catch (InvalidEmailException e)
        {
            formExceptions.Add("email", e.Message);
        }
        catch (FormValidationException e)
        {
            formExceptions.AddRange(e.Fields);
        }
        finally
        {
            OnFormException?.Invoke(this, new OnFormExceptionArguments { Exceptions = formExceptions.LimitToFirst() });
            Processing = false;
        }
    }

    [RelayCommand]
    private void GoToLoginPage()
    {
        router.GoTo<LoginPageViewModel>();
    }
}