
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Primitives;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Presentation.Controls;

namespace Presentation.ViewModels;

public abstract partial class AuthenticatedPageViewModel : ViewModelBase
{
    protected LogOutInteractor _logOutInteractor;
    
    public AuthenticatedPageViewModel(
        HistoryRouter<ViewModelBase> router, 
        LogOutInteractor logOutInteractor
    ) : base(router)
    {
        _logOutInteractor = logOutInteractor;
    }

    [RelayCommand]
    protected void HandleMenuClick(MainMenuItem item)
    {
        NavigateTo(item.Link);
    }

    [RelayCommand]
    protected void LogOut()
    {
        SendAction(async () =>
        {
            await _logOutInteractor.LogOut();
            _router.GoTo<LoginPageViewModel>();
        });
    }
    
    protected async Task SendAction(Func<Task> sendAction)
    {
        try
        {
            await sendAction.Invoke();
        }
        catch (UnauthorizedException e)
        {
            ShowNotification("Unauthorized", e.Message);
            _router.GoTo<LoginPageViewModel>();
        }
    }
}