using Application.Abstractions;
using Domain.Entities;
using Domain.Primitives;

namespace Application.UseCases;

public class CloseReportInteractor
{
    private IReportService _reportService;

    public CloseReportInteractor(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<bool> Close(Report report)
    {
        return await _reportService.Close(report);
    }
}