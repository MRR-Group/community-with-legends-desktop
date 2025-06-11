using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Enums;
using Moq;
using Xunit;

namespace Tests;

public class UnbanUserInteractorTests
{
    [Fact]
    public async Task Unban_ShouldThrow_WhenUserIsNotBanned()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var banService = new Mock<IBanService>();
        var interactor = new UnbanUserInteractor(banService.Object);

        await Assert.ThrowsAsync<UserNotBannedException>(() => interactor.Unban(user));
    }

    [Fact]
    public async Task Unban_ShouldCallService_WhenUserIsBanned()
    {
        var user = new User(2, "bannedUser", new Uri("https://example.com/avatar2.jpg"), new Email("banned@example.com"), true, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
            
        var banService = new Mock<IBanService>();
        banService.Setup(s => s.Unban(user)).ReturnsAsync(true);

        var interactor = new UnbanUserInteractor(banService.Object);
        var result = await interactor.Unban(user);

        Assert.True(result);
        banService.Verify(s => s.Unban(user), Times.Once);
    }
}
