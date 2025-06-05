using Application.Abstractions;
using Domain.Primitives;

namespace Application.UseCases;

public class RestoreReportableInteractor
{
    private IReportService _reportService;

    public RestoreReportableInteractor(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<bool> Restore(Reportable reportable)
    {
        return await _reportService.Restore(reportable);
    }
}