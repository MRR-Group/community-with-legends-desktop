using Domain.Enums;
using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities;

public class Report : Entity
{
    public string Reason { get; private set; }
    public Reportable Reportable { get; private set; }
    public Date ReportedAt { get; private set; }
    public User ReportedBy { get; private set; }
    public ReportStatus[] Status { get; private set; }
    public Date? ResolvedAt { get; private set; }
    
    public Report(uint id, string reason, Reportable reportable, Date reportedAt, User reportedBy, ReportStatus[] status, Date? resolvedAt) : base(id)
    {
        Reason = reason;
        Reportable = reportable;
        ReportedAt = reportedAt;
        ReportedBy = reportedBy;
        Status = status;
        ResolvedAt = resolvedAt;
    }

    public bool IsDeleted
    {
        get => Status.Contains(ReportStatus.Deleted);
    }
    
    public bool IsResolved
    {
        get => !Status.Contains(ReportStatus.Pending);
    }
}
