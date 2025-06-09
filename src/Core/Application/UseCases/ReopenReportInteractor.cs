using Application.Abstractions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class ReopenReportInteractor
{
    private IReportService _reportService;

    public ReopenReportInteractor(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<bool> Reopen(Report report)
    {
        return await _reportService.Reopen(report);
    }
}