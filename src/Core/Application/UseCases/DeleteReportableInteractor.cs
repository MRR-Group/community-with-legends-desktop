using Application.Abstractions;
using Domain.Primitives;

namespace Application.UseCases;

public class DeleteReportableInteractor
{
    private IReportService _reportService;

    public DeleteReportableInteractor(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<bool> Delete(Reportable reportable)
    {
        return await _reportService.Delete(reportable);
    }
}