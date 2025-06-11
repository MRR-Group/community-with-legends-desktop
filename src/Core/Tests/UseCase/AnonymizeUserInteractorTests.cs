using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests;

public class AnonymizeUserInteractorTests
{
    [Fact]
    public async Task Anonymize_ShouldReturnTrue_WhenServiceReturnsTrue()
    {
        var user = new User(0, "Example", new Uri("https://example.com/avatar.jpg"), new Email("email@wp.pl"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));
        var mockService = new Mock<IAnonymizeService>();
        mockService.Setup(s => s.Anonymize(user)).ReturnsAsync(true);

        var interactor = new AnonymizeUserInteractor(mockService.Object);

        var result = await interactor.Anonymize(user);

        Assert.True(result);
        mockService.Verify(s => s.Anonymize(user), Times.Once);
    }

    [Fact]
    public async Task Anonymize_ShouldReturnFalse_WhenServiceReturnsFalse()
    {
        var user = new User(0, "Example", new Uri("https://example.com/avatar.jpg"), new Email("email@wp.pl"), false, new Roles([]), new Permissions([]), new Date("2025-06-11T10:15:28.000000Z"));
        var mockService = new Mock<IAnonymizeService>();
        mockService.Setup(s => s.Anonymize(user)).ReturnsAsync(false);

        var interactor = new AnonymizeUserInteractor(mockService.Object);

        var result = await interactor.Anonymize(user);

        Assert.False(result);
        mockService.Verify(s => s.Anonymize(user), Times.Once);
    }
}
