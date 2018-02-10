namespace FakeBet.Tests
{
    using System;
    using System.Collections.Generic;
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
            var repoMock = this.GetUserRepoMockWithSampleData();
            var userService = new UserService(repoMock.Object);
            var user = new User();

            await userService.RegisterUserAsync(user);

            Console.Write(repoMock.Object.Users.Count());

            repoMock.Verify(act => act.RegisterUserAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task CanGetUser()
        {
            var repoMock = this.GetUserRepoMockWithSampleData();
            var userService = new UserService(repoMock.Object);

            await userService.GetUserAsync("user1");

            repoMock.Verify(m => m.GetUserAsync(It.IsAny<string>()), Times.Once());
        }

        private Mock<IUserRepository> GetUserRepoMockWithSampleData()
        {
            var repoMock = new Mock<IUserRepository>();
            var random = new Random();
            repoMock.Setup(m => m.Users).Returns(
                (new[]
                     {
                         new User
                             {
                                 CreateTime = DateTime.Now,
                                 Email = "email1@email.pl",
                                 NickName = "user1",
                                 Password = "password1",
                                 Points = random.Next(0, 10000),
                                 Salt = "salt",
                                 Status = UserStatus.Active,
                                 VotesHistory = new List<Vote>()
                             },
                         new User
                             {
                                 CreateTime = DateTime.Now,
                                 Email = "email2@email.pl",
                                 NickName = "user2",
                                 Password = "password2",
                                 Points = random.Next(0, 10000),
                                 Salt = "salt",
                                 Status = UserStatus.Active,
                                 VotesHistory = new List<Vote>()
                             },
                         new User
                             {
                                 CreateTime = DateTime.Now,
                                 Email = "email3@email.pl",
                                 NickName = "user3",
                                 Password = "password3",
                                 Points = random.Next(0, 10000),
                                 Salt = "salt",
                                 Status = UserStatus.Active,
                                 VotesHistory = new List<Vote>()
                             }
                     }).AsQueryable());
            return repoMock;
        }
    }
}