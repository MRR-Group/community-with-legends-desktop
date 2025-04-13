using Domain.Entities;

namespace Infrastructure.DTOs;

public class LoginResponseDto
{
    public User User { get; }

    public LoginResponseDto(User user)
    {
        User = user;
    }
}