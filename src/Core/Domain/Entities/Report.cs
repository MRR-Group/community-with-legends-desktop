using Domain.Primitives;
using Domain.ValueObjects;
namespace Domain.Entities;

public class Report : Entity
{
    public string Reason { get; private set; }
    public Reportable Reportable { get; private set; }
    public Date ReportedAt { get; private set; }
    public User ReportedBy { get; private set; }

    public Report(uint id, string reason, Reportable reportable, Date reportedAt, User reportedBy) : base(id)
    {
        Reason = reason;
        Reportable = reportable;
        ReportedAt = reportedAt;
        ReportedBy = reportedBy;
    }
}
