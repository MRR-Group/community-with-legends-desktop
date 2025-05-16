using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    public PermissionRepository PermissionRepository { get; private set; }
    private UserRepository _userRepository;
    private BanUserInteractor _banUserInteractor;
    private UnbanUserInteractor _unbanUserInteractor;
    private AnonymizeUserInteractor _anonymizeUserInteractor;
    private GiveModeratorRoleInteractor _giveModeratorRoleInteractor;
    private RevokeModeratorRoleInteractor _revokeModeratorRoleInteractor;
    private RevokeAdministratorRoleInteractor _revokeAdministratorRoleInteractor;
    
    public UsersPageViewModel(
        HistoryRouter<ViewModelBase> router, 
        UserRepository userRepository,
        PermissionRepository permissionRepository,
        BanUserInteractor banUserInteractor, 
        UnbanUserInteractor unbanUserInteractor, 
        AnonymizeUserInteractor anonymizeUserInteractor,
        GiveModeratorRoleInteractor giveModeratorRoleInteractor,
        RevokeAdministratorRoleInteractor revokeAdministratorRoleInteractor,
        RevokeModeratorRoleInteractor revokeModeratorRoleInteractor
    ) : base(router)
    {
        _userRepository = userRepository;
        _banUserInteractor = banUserInteractor;
        _unbanUserInteractor = unbanUserInteractor;
        _anonymizeUserInteractor = anonymizeUserInteractor;
        _giveModeratorRoleInteractor = giveModeratorRoleInteractor;
        _revokeModeratorRoleInteractor = revokeModeratorRoleInteractor;
        _revokeAdministratorRoleInteractor = revokeAdministratorRoleInteractor;
        
        PermissionRepository = permissionRepository;
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
        
    [RelayCommand]
    private void GrantModeratorRole(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _giveModeratorRoleInteractor.GiveRole(user);
            ShowNotification("Success", $"Moderator privileges granted to {user.Name}");
        });
    }
    
    [RelayCommand]
    private void RevokeModeratorRole(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _revokeModeratorRoleInteractor.RevokeRole(user);
            ShowNotification("Success", $"Moderator revoke from {user.Name}");
        });
    }
    
    [RelayCommand]
    private void RevokeAdministratorRole(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _revokeAdministratorRoleInteractor.RevokeRole(user);
            ShowNotification("Success", $"Administrator revoke from {user.Name}");
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