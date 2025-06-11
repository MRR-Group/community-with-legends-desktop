using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class ReportRepository : Repository<Report, ReportDto>
{
    public ReportRepository(CookieSession session): base(session, "reports")
    {
    }
}
