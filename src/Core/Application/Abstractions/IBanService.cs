using Domain.Entities;

namespace Application.Abstractions;

public interface IBanService
{
    public Task<bool> Ban(User user);
    public Task<bool> Unban(User user);
}