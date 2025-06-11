using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.UseCases;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests.UseCase;

public class CreateAdministratorUserInteractorTests
{
    [Fact]
    public async Task CreateAdministrator_ShouldReturnAdministrator_WhenServiceSucceeds()
    {
        var name = "admin";
        var email = new Email("admin@example.com");
        var password = new Password("securePassword123");
        var expectedAdmin = new Administrator(
            1,
            name,
            new Uri("https://example.com/avatar.jpg"),
            email,
            false,
            new Roles([]),
            new Permissions([]),
            new Date("2025-06-11T00:00:00Z")
        );

        var mockService = new Mock<IAdministratorService>();
        mockService
            .Setup(s => s.CreateAdministrator(name, email, password))
            .ReturnsAsync(expectedAdmin);

        var interactor = new CreateAdministratorUserInteractor(mockService.Object);

        var result = await interactor.CreateAdministrator(name, email, password);

        Assert.Equal(expectedAdmin, result);
        mockService.Verify(s => s.CreateAdministrator(name, email, password), Times.Once);
    }

    [Fact]
    public async Task CreateAdministrator_ShouldPropagateException_WhenServiceThrows()
    {
        var name = "admin";
        var email = new Email("admin@example.com");
        var password = new Password("securePassword123");

        var mockService = new Mock<IAdministratorService>();
        mockService
            .Setup(s => s.CreateAdministrator(name, email, password))
            .ThrowsAsync(new Exception("Service error"));

        var interactor = new CreateAdministratorUserInteractor(mockService.Object);

        await Assert.ThrowsAsync<Exception>(() =>
            interactor.CreateAdministrator(name, email, password));
    }
}