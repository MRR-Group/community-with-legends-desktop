using Domain.Entities;
using Domain.Primitives;

namespace Application.Abstractions;

public interface IReportService
{
    public Task<bool> Delete(Reportable reportable);
    public Task<bool> Restore(Reportable reportable);
}