using Application.Abstractions;
using Application.UseCases;
using Moq;

namespace Tests.UseCase;

public class LogOutInteractorTests
{
    [Fact]
    public async Task LogOut_ShouldCallService_WhenServiceSucceeds()
    {
        var mockService = new Mock<ILogOutService>();
        mockService.Setup(s => s.LogOut()).Returns(Task.CompletedTask);
        var interactor = new LogOutInteractor(mockService.Object);
        await interactor.LogOut();
        mockService.Verify(s => s.LogOut(), Times.Once);
    }

    [Fact]
    public async Task LogOut_ShouldPropagateException_WhenServiceThrows()
    {
        var mockService = new Mock<ILogOutService>();
        mockService.Setup(s => s.LogOut()).ThrowsAsync(new Exception("Service error"));
        var interactor = new LogOutInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.LogOut());
    }
}

