namespace FakeBet.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;
    using FakeBet.Services.Implementations;
    using FakeBet.Services.Interfaces;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class UserServiceTest
    {
        [Fact]
        public async Task CanRegisterUser()
        {
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(m => m.Users).Returns((new[] { new User { } }).AsQueryable());
            var userService = new UserService(repoMock.Object);
            var user = new User();

            await userService.RegisterUserAsync(user);

            Console.Write(repoMock.Object.Users.Count());

            repoMock.Verify(act => act.RegisterUserAsync(It.IsAny<User>()));
        }
    }
}