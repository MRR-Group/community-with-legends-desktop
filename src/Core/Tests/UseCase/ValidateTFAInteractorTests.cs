using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class ValidateTFAInteractorTests
{
    [Fact]
    public async Task Validate_ShouldCallService_WhenServiceSucceeds()
    {
        var mockService = new Mock<ITFAService>();
        mockService.Setup(s => s.Validate("token")).Returns(Task.CompletedTask);
        var interactor = new ValidateTFAInteractor(mockService.Object);
        await interactor.Validate("token");
        mockService.Verify(s => s.Validate("token"), Times.Once);
    }

    [Fact]
    public async Task Validate_ShouldPropagateException_WhenServiceThrows()
    {
        var mockService = new Mock<ITFAService>();
        mockService.Setup(s => s.Validate("token")).ThrowsAsync(new Exception("Service error"));
        var interactor = new ValidateTFAInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.Validate("token"));
    }
}

