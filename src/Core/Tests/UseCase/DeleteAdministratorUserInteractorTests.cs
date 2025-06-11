using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class DeleteAdministratorUserInteractorTests
{
    [Fact]
    public async Task Delete_ShouldCallService_WhenServiceSucceeds()
    {
        var user = new Administrator(
            0, 
            "TestUser", 
            new Uri("https://example.com/avatar.jpg"), 
            new Domain.ValueObjects.Email("email@wp.pl"), 
            false,
            new Domain.ValueObjects.Roles([Role.Administrator]), 
            new Domain.ValueObjects.Permissions([Permission.ManageAdministrators]), 
            new Domain.ValueObjects.Date("2025-06-11T10:15:28.000000Z")
        );
        
        var mockService = new Mock<IAdministratorService>();
        mockService.Setup(s => s.DeleteAdministrator(user)).ReturnsAsync(true);
        
        var interactor = new DeleteAdministratorInteractor(mockService.Object);
        await interactor.DeleteAdministrator(user);
        
        mockService.Verify(s => s.DeleteAdministrator(user), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldPropagateException_WhenServiceThrows()
    {
        var user = new Administrator(
            0, 
            "TestUser", 
            new Uri("https://example.com/avatar.jpg"), 
            new Domain.ValueObjects.Email("email@wp.pl"), 
            false,
            new Domain.ValueObjects.Roles([Role.Administrator]), 
            new Domain.ValueObjects.Permissions([Permission.ManageAdministrators]), 
            new Domain.ValueObjects.Date("2025-06-11T10:15:28.000000Z")
        );
        
        var mockService = new Mock<IAdministratorService>();
        mockService.Setup(s => s.DeleteAdministrator(user)).ThrowsAsync(new Exception("Service error"));
        var interactor = new DeleteAdministratorInteractor(mockService.Object);
        await Assert.ThrowsAsync<Exception>(() => interactor.DeleteAdministrator(user));
    }
}

