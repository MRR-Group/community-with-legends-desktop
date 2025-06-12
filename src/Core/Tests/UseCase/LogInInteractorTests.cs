using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;

namespace Tests.UseCase;

public class LogInInteractorTests
{
    [Fact]
    public async Task LogIn_ShouldReturnUser_WhenServiceSucceeds()
    {
        var email = new Email("user@example.com");
        var password = new Password("Password123!");
        var expectedUser = new User(1, "user", new Uri("https://example.com/avatar.jpg"), email, false, new Roles([]), new Permissions([]), new Date("2025-06-11T00:00:00Z"));

        var mockService = new Mock<ILogInService>();
        mockService.Setup(s => s.LogIn(email, password)).ReturnsAsync(expectedUser);

        var interactor = new LogInInteractor(mockService.Object);
        var result = await interactor.LogIn(email, password);

        Assert.Equal(expectedUser, result);
        mockService.Verify(s => s.LogIn(email, password), Times.Once);
    }

    [Fact]
    public async Task LogIn_ShouldPropagateException_WhenServiceThrows()
    {
        var email = new Email("user@example.com");
        var password = new Password("Password123!");
        var mockService = new Mock<ILogInService>();
        mockService.Setup(s => s.LogIn(email, password)).ThrowsAsync(new Exception("Service error"));
        var interactor = new LogInInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.LogIn(email, password));
    }
}

