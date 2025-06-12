using Infrastructure.DTOs;

namespace Tests.DTOs;

public class AdminsDtoTests
{
    [Fact]
    public void ToEntity_ShouldMapAllPropertiesCorrectly()
    {
        var dto = new AdministratorDto()
        {
            Id = 1,
            Name = "AdminUser",
            Email = "admin@example.com",
            Avatar = "https://example.com/avatar.jpg",
            IsBanned = false,
            Roles = ["Administrator"],
            Permissions = ["BanUsers"],
            CreationDate = "2025-06-11T00:00:00Z"
        };

        var entity = dto.ToEntity();

        Assert.Equal(dto.Id, entity.Id);
        Assert.Equal(dto.Name, entity.Name);
        Assert.Equal(dto.Email, entity.Email.Value);
        Assert.Equal(dto.Avatar, entity.Avatar.ToString());
        Assert.Equal(dto.IsBanned, entity.IsBanned);
        Assert.Equal("2025.06.11 00:00", entity.CreationDate.ToString());
        Assert.Contains(Domain.Enums.Role.Administrator, entity.Roles.Value);
        Assert.Contains(Domain.Enums.Permission.BanUsers, entity.Permissions.Value);
    }
}

