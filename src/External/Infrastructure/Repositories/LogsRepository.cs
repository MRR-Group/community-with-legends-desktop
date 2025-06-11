using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class LogsRepository : Repository<Log, LogDto>
{
    public LogsRepository(CookieSession session) : base(session, "logs")
    {
    }
}
