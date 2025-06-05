    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
using Application.UseCases;
using Avalonia.SimpleRouter;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using Infrastructure.Repositories;
using Presentation.Controls;
using Ursa.Controls;

namespace Presentation.ViewModels;

public partial class ReportsPageViewModel : DataPageViewModel<Report>
{
        
    public PermissionRepository PermissionRepository { get; private set; }
    private ReportRepository _reportRepository;
    private BanUserInteractor _banUserInteractor;
    private UnbanUserInteractor _unbanUserInteractor;
    private DeleteReportableInteractor _deleteReportableInteractor;
    private RestoreReportableInteractor _restoreReportableInteractor;
    
    public ReportsPageViewModel(
        HistoryRouter<ViewModelBase> router,
        LogOutInteractor logOutInteractor,
        BanUserInteractor banUserInteractor, 
        UnbanUserInteractor unbanUserInteractor,
        DeleteReportableInteractor deleteReportableInteractor,
        RestoreReportableInteractor restoreReportableInteractor,
        ReportRepository reportRepository,
        PermissionRepository permissionRepository
    ) : base(router, logOutInteractor)
    {
        PermissionRepository = permissionRepository;
        _reportRepository = reportRepository;
        _banUserInteractor = banUserInteractor;
        _unbanUserInteractor = unbanUserInteractor;
        _deleteReportableInteractor = deleteReportableInteractor;
        _restoreReportableInteractor = restoreReportableInteractor;
        
        RefreshData();
    }
    
    [RelayCommand]
    private void Unban(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _unbanUserInteractor.Unban(report.Reportable.User);
            ShowNotification("Success", $"{report.Reportable.User.Name} unbanned successfully");
        });
    }
    
    [RelayCommand]
    private void Ban(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _banUserInteractor.Ban(report.Reportable.User);
            ShowNotification("Success", $"{report.Reportable.User.Name} banned successfully");
        });
    }
        
    [RelayCommand]
    private void Delete(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _deleteReportableInteractor.Delete(report.Reportable);
            ShowNotification("Success", "Item deleted successfully");
        });
    }

    [RelayCommand]
    private void Restore(Report? target)
    {
        SendAction(target, async (report) =>
        {
            await _banUserInteractor.Ban(report.Reportable.User);
            ShowNotification("Success", $"{report.Reportable.User.Name} banned successfully");
        });
    }

    
    protected override async Task RefreshData()
    {
        try
        {
            var reports = await _reportRepository.All();

            Data.Clear();

            foreach (var report in reports)
            {
                if (report is null)
                {
                    continue;
                }

                Data.Add(report);
            }
        }
        catch (Exception e)
        {
            var ee = e;
        }
    }
}