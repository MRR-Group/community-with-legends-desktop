using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class RevokeAdministratorRoleInteractorTests
{
    [Fact]
    public async Task Revoke_ShouldCallService_WhenServiceSucceeds()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([Role.Administrator]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var mockService = new Mock<IAdministratorService>();
        mockService.Setup(s => s.RevokeAdministratorRole(user)).ReturnsAsync(true);
        var interactor = new RevokeAdministratorRoleInteractor(mockService.Object);
        await interactor.RevokeRole(user);
        mockService.Verify(s => s.RevokeAdministratorRole(user), Times.Once);
    }

    [Fact]
    public async Task Revoke_ShouldPropagateException_WhenServiceThrows()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([Role.Administrator]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var mockService = new Mock<IAdministratorService>();
        mockService.Setup(s => s.RevokeAdministratorRole(user)).ThrowsAsync(new Exception("Service error"));
        var interactor = new RevokeAdministratorRoleInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.RevokeRole(user));
    }
}

