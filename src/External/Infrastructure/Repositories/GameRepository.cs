using Domain.Entities;
using Flurl.Http;
using Infrastructure.DTOs;
namespace Infrastructure.Repositories;

public class GameRepository : Repository<Game, GameDto>
{
    public GameRepository(CookieSession session) : base(session, "games")
    {
    }
}
