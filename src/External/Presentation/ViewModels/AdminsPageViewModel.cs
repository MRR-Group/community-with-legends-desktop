using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Presentation.Dialogs;
using Presentation.ViewModels.Dialogs;
using Ursa.Common;
using Ursa.Controls;
using Ursa.Controls.Options;

namespace Presentation.ViewModels;

public partial class AdminsPageViewModel : DataPageViewModel<Administrator>
{
    private RevokeAdministratorRoleInteractor _revokeAdministratorRoleInteractor;
    private CreateAdministratorUserInteractor _createAdministratorUserInteractor;
    private DeleteAdministratorInteractor _deleteAdministratorInteractor;

    private string? _email;
    private string? _name;
    private string? _password;
    
    public AdminsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogOutInteractor logOutInteractor,
        AdminRepository adminRepository,
        RevokeAdministratorRoleInteractor revokeAdministratorRoleInteractor,
        CreateAdministratorUserInteractor createAdministratorUserInteractor,
        DeleteAdministratorInteractor deleteAdministratorInteractor
    ) : base(router, adminRepository, logOutInteractor)
    {
        _revokeAdministratorRoleInteractor = revokeAdministratorRoleInteractor;
        _createAdministratorUserInteractor = createAdministratorUserInteractor;
        _deleteAdministratorInteractor = deleteAdministratorInteractor;
    }
    
    [RelayCommand]
    private void RevokeAdministratorRole(Administrator? target)
    {
        SendAction(target, async (admin) =>
        {
            await _revokeAdministratorRoleInteractor.RevokeRole(admin);
            RemoveItem(admin);
            
            ShowNotification("Success", $"Administrator revoke from {admin.Name}.");
        });
    }

    [RelayCommand]
    public async Task ShowCreateAdminDialog()
    {
        var options = new DrawerOptions()
        {
            Position = Position.Right,
            Buttons = DialogButton.OKCancel,
            CanLightDismiss = false,
            IsCloseButtonVisible = true,
            Title = "Create Admin",
            CanResize = false,
        };
        
        var vm = new CreateAdminDialogViewModel(_name, _email, _password);
        var result = await Drawer.ShowModal<CreateAdminDialog, CreateAdminDialogViewModel>(vm, "root", options);

        SaveForm(vm);

        if (result == DialogResult.OK && ValidateAdminDialog(vm))
        {
            CreateAdministrator(vm.Name ?? "", vm.Email ?? "", vm.Password ?? "");
        }
        else if (result == DialogResult.Cancel)
        {
            ClearForm();
        }
    }
    
    private void SaveForm(CreateAdminDialogViewModel vm)
    {
        _name = vm.Name;
        _email = vm.Email;
        _password = vm.Password;
    }

    private void ClearForm()
    {
        _name = null;
        _email = null;
        _password = null;
    }

    private bool ValidateAdminDialog(CreateAdminDialogViewModel vm)
    {
        if (vm.Name is not { Length: > 0 })
        {
            ShowNotification("Error", "Name is required");
            return false;
        }
        
        if (vm.Email is not { Length: > 0 })
        {
            ShowNotification("Error", "Email is required");
            return false;
        }
        
        if (vm.Password is not { Length: > 0 })
        {
            ShowNotification("Error", "Password is required");
            return false;
        }
        
        return true;
    }

    private void CreateAdministrator(string name, string email, string password)
    {
        SendAction(async () =>
        {
            try
            {
                var admin = await _createAdministratorUserInteractor.CreateAdministrator(name, email, password);
                AddItem(admin);

                ShowNotification("Success", $"Administrator {name} created.");
                ClearForm();
            }
            catch (FormValidationException exception)
            {
                foreach (var (_, messages) in exception.Fields)
                {
                    DisplayValidationErrors(messages);
                }
            }
            catch (InvalidPasswordException exception)
            {
                ShowNotification("Error", exception.Message);
            }
            catch (InvalidEmailException exception)
            {
                ShowNotification("Error", exception.Message);
            }
        });
    }

    private void DisplayValidationErrors(List<string> messages)
    {
        foreach (var message in messages)
        {
            ShowNotification("Error", message);
        }
    }

    [RelayCommand]
    private void DeleteAdministrator(Administrator? target)
    {
        SendAction(target, async (admin) =>
        {
            await _deleteAdministratorInteractor.DeleteAdministrator(admin);
            RemoveItem(admin);
            
            ShowNotification("Success", $"Administrator {admin.Name} deleted.");
        });
    }
}