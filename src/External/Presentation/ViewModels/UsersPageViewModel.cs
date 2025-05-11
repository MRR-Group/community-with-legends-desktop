using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.Exceptions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public partial class UsersPageViewModel : ViewModelBase
{
    public ObservableCollection<User> Users { get; private set;  }
    private UserRepository _userRepository;
    private BanUserInteractor _banUserInteractor;
    private UnbanUserInteractor _unbanUserInteractor;
    private AnonymizeUserInteractor _anonymizeUserInteractor;

    public ICommand HandleBanClick => new RelayCommand<User>(Ban);
    public ICommand HandleUnBanClick => new RelayCommand<User>(Unban);
    public ICommand HandleAnonymizeClick => new RelayCommand<User>(Anonymize);

    public UsersPageViewModel(
        HistoryRouter<ViewModelBase> router, 
        UserRepository userRepository, 
        BanUserInteractor banUserInteractor, 
        UnbanUserInteractor unbanUserInteractor, 
        AnonymizeUserInteractor anonymizeUserInteractor
    ) : base(router)
    {
        _userRepository = userRepository;
        _banUserInteractor = banUserInteractor;
        _unbanUserInteractor = unbanUserInteractor;
        _anonymizeUserInteractor = anonymizeUserInteractor;
        
        Users = [];
        RefreshUsers();
    }

    private async Task RefreshUsers()
    {
        var data = await _userRepository.All();
        
        Users.Clear();
        
        foreach (var user in data)
        {
            Users.Add(user);
        }
    }

    [RelayCommand]
    private void Anonymize(User target)
    {
        SendAction(target, async (user) =>
        {
            await _anonymizeUserInteractor.Anonymize(user);
            ShowNotification("Success", $"{user.Name} anonymized successfully");
        });
    }
    
    [RelayCommand]
    private void Unban(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _unbanUserInteractor.Unban(user);
            ShowNotification("Success", $"{user.Name} unbanned successfully");
        });
    }
    
    [RelayCommand]
    private void Ban(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _banUserInteractor.Ban(user);
            ShowNotification("Success", $"{user.Name} banned successfully");
        });
    }
    
    protected async Task SendAction(User? target, Func<User, Task> sendAction)
    {
        if (target == null)
        {
            return;
        }

        try
        {
            await sendAction.Invoke(target);
            await RefreshUsers();
        }
        catch (UnauthorizedException e)
        {
            ShowNotification("Unauthorized", e.Message);
        }
        catch (CannotBanNonUserException e)
        {
            ShowNotification("Error", e.Message);
        }
        catch (UserAlreadyBannedException e)
        {
            ShowNotification("Error", e.Message);
        }
        catch (UserNotBannedException e)
        {
            ShowNotification("Error", e.Message);
        }
    }
}