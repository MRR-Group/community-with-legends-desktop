using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class DeleteUserAvararInteractorTests
{
    [Fact]
    public async Task Delete_ShouldCallService_WhenServiceSucceeds()
    {
        var user = new Domain.Entities.User(1, "user", new Uri("https://example.com/avatar.jpg"), new Domain.ValueObjects.Email("user@example.com"), false, new Domain.ValueObjects.Roles([]), new Domain.ValueObjects.Permissions([]), new Domain.ValueObjects.Date("2025-06-11T00:00:00Z"));
        var mockService = new Mock<IProfileService>();
        mockService.Setup(s => s.DeleteAvatar(user)).ReturnsAsync(true);
        var interactor = new DeleteAvatarInteractor(mockService.Object);
        await interactor.DeleteAvatar(user);
        mockService.Verify(s => s.DeleteAvatar(user), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldPropagateException_WhenServiceThrows()
    {
        var user = new Domain.Entities.User(1, "user", new Uri("https://example.com/avatar.jpg"), new Domain.ValueObjects.Email("user@example.com"), false, new Domain.ValueObjects.Roles([]), new Domain.ValueObjects.Permissions([]), new Domain.ValueObjects.Date("2025-06-11T00:00:00Z"));
        var mockService = new Mock<IProfileService>();
        mockService.Setup(s => s.DeleteAvatar(user)).ThrowsAsync(new Exception("Service error"));
        var interactor = new DeleteAvatarInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.DeleteAvatar(user));
    }
}
