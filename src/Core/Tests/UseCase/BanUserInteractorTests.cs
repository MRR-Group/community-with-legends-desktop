using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Exceptions;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Xunit;

namespace Tests
{
    public class BanUserInteractorTests
    {
        [Fact]
        public async Task Ban_ShouldThrowCannotBanNonUserException_WhenUserHasBanUsersPermission()
        {
            var user = new User(
                0, 
                "TestUser", 
                new Uri("https://example.com/avatar.jpg"), 
                new Domain.ValueObjects.Email("email@wp.pl"), 
                false, 
                new Domain.ValueObjects.Roles([]), 
                new Domain.ValueObjects.Permissions([Permission.BanUsers]),
                new Domain.ValueObjects.Date("2025-06-11T10:15:28.000000Z")
            );
            
            var mockBanService = new Mock<IBanService>();

            var interactor = new BanUserInteractor(mockBanService.Object);

            await Assert.ThrowsAsync<CannotBanNonUserException>(() => interactor.Ban(user));

            mockBanService.Verify(s => s.Ban(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Ban_ShouldThrowUserAlreadyBannedException_WhenUserIsAlreadyBanned()
        {
            var user = new User(
                0, 
                "TestUser", 
                new Uri("https://example.com/avatar.jpg"), 
                new Domain.ValueObjects.Email("email@wp.pl"), 
                true,
                new Domain.ValueObjects.Roles([]), 
                new Domain.ValueObjects.Permissions([]), 
                new Domain.ValueObjects.Date("2025-06-11T10:15:28.000000Z")
            );
            
            var mockBanService = new Mock<IBanService>();

            var interactor = new BanUserInteractor(mockBanService.Object);

            await Assert.ThrowsAsync<UserAlreadyBannedException>(() => interactor.Ban(user));

            mockBanService.Verify(s => s.Ban(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Ban_ShouldReturnTrue_WhenUserCanBeBanned()
        {
            var user = new User(
                0, 
                "TestUser", 
                new Uri("https://example.com/avatar.jpg"), 
                new Domain.ValueObjects.Email("email@wp.pl"), 
                false,
                new Domain.ValueObjects.Roles([]), 
                new Domain.ValueObjects.Permissions([]), 
                new Domain.ValueObjects.Date("2025-06-11T10:15:28.000000Z")
            );

            var mockBanService = new Mock<IBanService>();
            mockBanService.Setup(s => s.Ban(user)).ReturnsAsync(true);

            var interactor = new BanUserInteractor(mockBanService.Object);
            
            var result = await interactor.Ban(user);

            Assert.True(result);
            mockBanService.Verify(s => s.Ban(user), Times.Once);
        }
    }
}
