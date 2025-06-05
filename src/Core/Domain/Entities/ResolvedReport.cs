using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities;

public class ResolvedReport : Report
{
    public Date ResolvedAt { get; private set; }
    public ReportStatus[] Status { get; private set; }
    
    public ResolvedReport(uint id, string reason, Reportable reportable, Date reportedAt, Date resolvedAt, ReportStatus[] status, User reportedBy) : base(id, reason, reportable, reportedAt,reportedBy)
    {
        ResolvedAt = resolvedAt;
        Status = status;
    }
}
