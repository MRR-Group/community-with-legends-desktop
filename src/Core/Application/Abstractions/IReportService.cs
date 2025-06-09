using Domain.Entities;
using Domain.Primitives;

namespace Application.Abstractions;

public interface IReportService
{
    public Task<bool> Close(Report report);
    public Task<bool> Reopen(Report report);
}