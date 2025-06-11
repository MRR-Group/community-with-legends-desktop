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

public class GiveModeratorRoleInteractorTests
{
    [Fact]
    public async Task GiveRole_ShouldThrow_WhenUserAlreadyIsModerator()
    {
        var roles = new Roles([Role.Moderator]);
        var user = new User(1, "mod", new Uri("https://example.com/avatar.jpg"), new Email("mod@example.com"), false, roles, new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));

        var roleService = new Mock<IRoleService>();
        var interactor = new GiveModeratorRoleInteractor(roleService.Object);

        await Assert.ThrowsAsync<UserAlreadyHasRoleException>(() => interactor.GiveRole(user));
    }

    [Fact]
    public async Task GiveRole_ShouldCallService_WhenUserIsNotModerator()
    {
        var roles = new Roles([]);
        var user = new User(2, "user", new Uri("https://example.com/avatar2.jpg"), new Email("user@example.com"), false, roles, new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));

        var roleService = new Mock<IRoleService>();
        roleService.Setup(s => s.GiveModeratorRole(user)).ReturnsAsync(true);

        var interactor = new GiveModeratorRoleInteractor(roleService.Object);
        var result = await interactor.GiveRole(user);

        Assert.True(result);
        roleService.Verify(s => s.GiveModeratorRole(user), Times.Once);
    }
}
