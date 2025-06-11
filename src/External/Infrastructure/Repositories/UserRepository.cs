using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class UserRepository : Repository<User, UserDto>
{
    public UserRepository(CookieSession session): base(session, "users")
    {
    }
}
