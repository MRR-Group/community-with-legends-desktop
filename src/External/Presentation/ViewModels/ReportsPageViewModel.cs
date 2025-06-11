using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.DTOs;
using Infrastructure.Repositories;

namespace Presentation.ViewModels;

public partial class ReportsPageViewModel : DataPageViewModel<Report>
{
    public PermissionRepository PermissionRepository { get; private set; }
    private BanUserInteractor _banUserInteractor;
    private UnbanUserInteractor _unbanUserInteractor;
    private DeleteReportableInteractor _deleteReportableInteractor;
    private RestoreDeleteReportableInteractor _restoreDeleteReportableInteractor;
    private ReopenReportInteractor _reopenReportInteractor;
    private CloseReportInteractor _closeReportInteractor;
    private RenameUserInteractor _renameUserInteractor;
    private DeleteAvatarInteractor _deleteAvatarInteractor;
    private DeleteUserHardwareInteractor _deleteUserHardwareInteractor;
    private RevokeAdministratorRoleInteractor _revokeAdministratorRoleInteractor;
    private RevokeModeratorRoleInteractor _revokeModeratorRoleInteractor;

    public ReportsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogOutInteractor logOutInteractor,
        BanUserInteractor banUserInteractor, 
        UnbanUserInteractor unbanUserInteractor,
        DeleteReportableInteractor deleteReportableInteractor,
        RestoreDeleteReportableInteractor restoreDeleteReportableInteractor,
        ReopenReportInteractor reopenReportInteractor,
        CloseReportInteractor closeReportInteractor,
        RenameUserInteractor renameUserInteractor,
        DeleteAvatarInteractor deleteAvatarInteractor, 
        DeleteUserHardwareInteractor deleteUserHardwareInteractor,
        RevokeAdministratorRoleInteractor revokeAdministratorRoleInteractor,
        RevokeModeratorRoleInteractor revokeModeratorRoleInteractor,
        ReportRepository reportRepository,
        PermissionRepository permissionRepository
    ) : base(router, reportRepository, logOutInteractor)
    {
        PermissionRepository = permissionRepository;
        _banUserInteractor = banUserInteractor;
        _unbanUserInteractor = unbanUserInteractor;
        _deleteReportableInteractor = deleteReportableInteractor;
        _restoreDeleteReportableInteractor = restoreDeleteReportableInteractor;
        _closeReportInteractor = closeReportInteractor;
        _reopenReportInteractor = reopenReportInteractor;
        _renameUserInteractor = renameUserInteractor;
        _deleteAvatarInteractor = deleteAvatarInteractor;
        _deleteUserHardwareInteractor = deleteUserHardwareInteractor;
        _revokeAdministratorRoleInteractor = revokeAdministratorRoleInteractor;
        _revokeModeratorRoleInteractor = revokeModeratorRoleInteractor;
    }
    
    [RelayCommand]
    private void Unban(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _unbanUserInteractor.Unban(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name} has been unbanned.");
        });
    }

    [RelayCommand]
    private void Ban(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _banUserInteractor.Ban(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name} has been banned.");
        });
    }

    [RelayCommand]
    private void Delete(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _deleteReportableInteractor.Delete(report.Reportable);
            await RefreshItem(report);
            
            ShowNotification("Success", "Item deleted successfully.");
        });
    }

    [RelayCommand]
    private void Restore(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _restoreDeleteReportableInteractor.Restore(report.Reportable);
            await RefreshItem(report);

            ShowNotification("Success", "Item restored successfully.");
        });
    }

    [RelayCommand]
    private void Close(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _closeReportInteractor.Close(report);
            await RefreshItem(report);

            ShowNotification("Success", "Report closed successfully.");
        });
    }

    [RelayCommand]
    private void Reopen(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _reopenReportInteractor.Reopen(report);
            await RefreshItem(report);

            ShowNotification("Success", "Report reopened successfully.");
        });
    }

    [RelayCommand]
    private void Rename(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _renameUserInteractor.Rename(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name} has been renamed.");
        });
    }

    [RelayCommand]
    void DeleteAvatar(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _deleteAvatarInteractor.DeleteAvatar(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name}'s avatar has been deleted.");
        });
    }

    [RelayCommand]
    private void DeleteHardware(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _deleteUserHardwareInteractor.DeleteHardware(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name}'s hardware info has been deleted.");
        });
    }

    [RelayCommand]
    private void RevokeAdmin(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _revokeAdministratorRoleInteractor.RevokeRole(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name} is no longer an administrator.");
        });
    }

    [RelayCommand]
    private void RevokeModerator(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _revokeModeratorRoleInteractor.RevokeRole(report.Reportable.User);
            await RefreshItem(report);

            ShowNotification("Success", $"{report.Reportable.User.Name} is no longer a moderator.");
        });
    }
}