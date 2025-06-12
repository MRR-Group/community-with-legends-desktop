using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class RenameUserInteractorTests
{
    [Fact]
    public async Task Rename_ShouldReturnTrue_WhenServiceSucceeds()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var mockService = new Mock<IProfileService>();
        mockService.Setup(s => s.Rename(user)).ReturnsAsync(true);
        var interactor = new RenameUserInteractor(mockService.Object);
        var result = await interactor.Rename(user);
        Assert.True(result);
        mockService.Verify(s => s.Rename(user), Times.Once);
    }

    [Fact]
    public async Task Rename_ShouldPropagateException_WhenServiceThrows()
    {
        var user = new User(1, "user", new Uri("https://example.com/avatar.jpg"), new Email("user@example.com"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));
        var mockService = new Mock<IProfileService>();
        mockService.Setup(s => s.Rename(user)).ThrowsAsync(new Exception("Service error"));
        var interactor = new RenameUserInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.Rename(user));
    }
}

