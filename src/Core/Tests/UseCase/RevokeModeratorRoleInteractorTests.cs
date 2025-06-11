using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests;

public class RevokeModeratorRoleInteractorTests
{
    [Fact]
    public async Task RevokeRole_ShouldThrow_WhenUserIsNotModerator()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var roleService = new Mock<IRoleService>();
        var interactor = new RevokeModeratorRoleInteractor(roleService.Object);

        await Assert.ThrowsAsync<UserDoesNotHaveRoleException>(() => interactor.RevokeRole(user));
    }

    [Fact]
    public async Task RevokeRole_ShouldCallService_WhenUserIsModerator()
    {
        var user = new User(2, "mod", new Uri("https://example.com/avatar2.jpg"), new Email("mod@example.com"), false, new Roles([Role.Moderator]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var roleService = new Mock<IRoleService>();
        roleService.Setup(s => s.RevokeModeratorRole(user)).ReturnsAsync(true);

        var interactor = new RevokeModeratorRoleInteractor(roleService.Object);
        var result = await interactor.RevokeRole(user);

        Assert.True(result);
        roleService.Verify(s => s.RevokeModeratorRole(user), Times.Once);
    }
}
