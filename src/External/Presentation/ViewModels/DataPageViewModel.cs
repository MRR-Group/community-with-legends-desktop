using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Exceptions;
using Presentation.Controls;

namespace Presentation.ViewModels;

public abstract partial class DataPageViewModel<T> : ViewModelBase
{
    public ObservableCollection<T> Data { get; protected set;  }
    protected LogOutInteractor _logOutInteractor;
    
    public DataPageViewModel(
        HistoryRouter<ViewModelBase> router, 
        LogOutInteractor logOutInteractor
    ) : base(router)
    {
        _logOutInteractor = logOutInteractor;
        Data = [];
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

    protected abstract Task RefreshData();
    
    protected async Task SendAction(Func<Task> sendAction)
    {
        try
        {
            await sendAction.Invoke();
            await RefreshData();
        }
        catch (UnauthorizedException e)
        {
            ShowNotification("Unauthorized", e.Message);
            _router.GoTo<LoginPageViewModel>();
        }
    }
    
    protected async Task SendAction(T? target, Func<T, Task> sendAction)
    {
        if (target == null)
        {
            return;
        }

        try
        {
            await sendAction.Invoke(target);
            await RefreshData();
        }
        catch (UnauthorizedException e)
        {
            ShowNotification("Unauthorized", e.Message);
            _router.GoTo<LoginPageViewModel>();
        }
    }
}