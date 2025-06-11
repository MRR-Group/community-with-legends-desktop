using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Application.UseCases;
using Domain.ValueObjects;
using Moq;
using Xunit;

namespace Tests;

public class RegisterInteractorTests
{
    [Fact]
    public async Task Register_ShouldThrow_WhenPasswordsDoNotMatch()
    {
        var registerService = new Mock<IRegisterService>();
        var interactor = new RegisterInteractor(registerService.Object);

        var password = new Password("Password123!");
        var repeat = new Password("Different123!");

        await Assert.ThrowsAsync<PasswordsDoNotMatchException>(() =>
            interactor.Register("user", new Email("user@example.com"), password, repeat));
    }

    [Fact]
    public async Task Register_ShouldCallService_WhenPasswordsMatch()
    {
        var registerService = new Mock<IRegisterService>();
        var interactor = new RegisterInteractor(registerService.Object);

        var email = new Email("user@example.com");
        var password = new Password("Password123!");

        await interactor.Register("user", email, password, password);

        registerService.Verify(s => s.Register("user", email, password), Times.Once);
    }
}
