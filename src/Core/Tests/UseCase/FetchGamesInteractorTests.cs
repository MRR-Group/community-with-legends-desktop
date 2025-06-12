using Application.Abstractions;
using Application.UseCases;
using Moq;

namespace Tests.UseCase;

public class FetchGamesInteractorTests
{
    [Fact]
    public async Task Fetch_ShouldReturnInt_WhenServiceSucceeds()
    {
        var mockService = new Mock<IGameService>();
        mockService.Setup(s => s.Fetch()).ReturnsAsync(42);
        var interactor = new FetchGamesInteractor(mockService.Object);
        var result = await interactor.Fetch();
        Assert.Equal(42, result);
        mockService.Verify(s => s.Fetch(), Times.Once);
    }

    [Fact]
    public async Task Fetch_ShouldPropagateException_WhenServiceThrows()
    {
        var mockService = new Mock<IGameService>();
        mockService.Setup(s => s.Fetch()).ThrowsAsync(new Exception("Service error"));
        var interactor = new FetchGamesInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.Fetch());
    }
}

