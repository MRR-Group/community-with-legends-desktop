using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Presentation.Controls;

namespace Presentation.ViewModels;

public partial class UsersPageViewModel : DataPageViewModel<User>
{
    public PermissionRepository PermissionRepository { get; private set; }
    private BanUserInteractor _banUserInteractor;
    private UnbanUserInteractor _unbanUserInteractor;
    private AnonymizeUserInteractor _anonymizeUserInteractor;
    private GiveModeratorRoleInteractor _giveModeratorRoleInteractor;
    private RevokeModeratorRoleInteractor _revokeModeratorRoleInteractor;
    private RenameUserInteractor _renameUserInteractor;
    private DeleteAvatarInteractor _deleteAvatarInteractor;

    public UsersPageViewModel(
        HistoryRouter<ViewModelBase> router, 
        UserRepository userRepository,
        PermissionRepository permissionRepository,
        BanUserInteractor banUserInteractor, 
        UnbanUserInteractor unbanUserInteractor,
        LogOutInteractor logOutInteractor,
        AnonymizeUserInteractor anonymizeUserInteractor,
        GiveModeratorRoleInteractor giveModeratorRoleInteractor,
        RevokeModeratorRoleInteractor revokeModeratorRoleInteractor,
        RenameUserInteractor renameUserInteractor,
        DeleteAvatarInteractor deleteAvatarInteractor
    ) : base(router, userRepository, logOutInteractor)
    {
        _banUserInteractor = banUserInteractor;
        _unbanUserInteractor = unbanUserInteractor;
        _logOutInteractor = logOutInteractor;
        _anonymizeUserInteractor = anonymizeUserInteractor;
        _giveModeratorRoleInteractor = giveModeratorRoleInteractor;
        _revokeModeratorRoleInteractor = revokeModeratorRoleInteractor;
        _renameUserInteractor = renameUserInteractor;
        _deleteAvatarInteractor = deleteAvatarInteractor;

        PermissionRepository = permissionRepository;
    }
    
    [RelayCommand]
    private void Anonymize(User target)
    {
        SendAction(target, async (user) =>
        {
            await _anonymizeUserInteractor.Anonymize(user);
            await RefreshItem(user);
            
            ShowNotification("Success", $"{user.Name} anonymized successfully");
        });
    }
    
    [RelayCommand]
    private void Unban(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _unbanUserInteractor.Unban(user);
            await RefreshItem(user);

            ShowNotification("Success", $"{user.Name} unbanned successfully");
        });
    }
    
    [RelayCommand]
    private void Ban(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _banUserInteractor.Ban(user);
            await RefreshItem(user);

            ShowNotification("Success", $"{user.Name} banned successfully");
        });
    }
        
    [RelayCommand]
    private void GrantModeratorRole(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _giveModeratorRoleInteractor.GiveRole(user);
            await RefreshItem(user);

            ShowNotification("Success", $"Moderator privileges granted to {user.Name}");
        });
    }
    
    [RelayCommand]
    private void RevokeModeratorRole(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _revokeModeratorRoleInteractor.RevokeRole(user);
            await RefreshItem(user);

            ShowNotification("Success", $"Moderator revoke from {user.Name}");
        });
    }
    
    [RelayCommand]
    private void ChangeAvatar(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _deleteAvatarInteractor.DeleteAvatar(user);
            await RefreshItem(user);

            ShowNotification("Success", $"Avatar changed.");
        });
    }
    
    [RelayCommand]
    private void RenameUser(User? target)
    {
        SendAction(target, async (user) =>
        {
            await _renameUserInteractor.Rename(user);
            await RefreshItem(user);

            ShowNotification("Success", $"User name changed.");
        });
    }
    
    protected async Task SendAction(User? target, Func<User, Task> sendAction)
    {
        try
        {
            await base.SendAction(target, sendAction);
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