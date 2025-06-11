using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class AdminRepository : Repository<Administrator, AdministratorDto>
{
    public AdminRepository(CookieSession session) : base(session, "admins")
    {
    }
}
