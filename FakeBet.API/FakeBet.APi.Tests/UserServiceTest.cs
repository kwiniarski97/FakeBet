using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.Helpers;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Implementations;
using FakeBet.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Moq;
using Xunit;

namespace FakeBet.APi.Tests
{
    using FakeBet.API.DTO;

    public class UserServiceTest
    {
        private IMapper _mapper;

        private Mock<IUserRepository> _userRepoMock;

        private IUserService _userService;

        private IOptions<AppSettingsSecret> _options;

        private Mock<IEmailClient> _emailMock;

        private Mock<IMatchService> _matchMock;

        public UserServiceTest()
        {
            var autoMapperProfile = new AutoMapperConfig();
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(autoMapperProfile); });
            _mapper = config.CreateMapper();

            _userRepoMock = new Mock<IUserRepository>();

            var options = new AppSettingsSecret() {Secret = "secretmustbelongerthat128bits"};
            _options = Options.Create(options);

            _emailMock = new Mock<IEmailClient>();

            _matchMock = new Mock<IMatchService>();
        }

        [Fact]
        public async Task GetUserByNickName()
        {
            SetArrangments();

            var user = await _userService.GetUserAsync("nickname");

            Assert.Equal("nickname", user.NickName);
        }

        [Fact]
        public async Task Get_User_Should_Return_Null_When_User_Doesnt_Exist()
        {
            SetArrangments();

            var user = await _userService.GetUserAsync("non existent user id");

            _userRepoMock.Verify(x => x.GetUserWithoutBetsAsync(It.IsAny<string>()), Times.Once);
            Assert.Null(user);
        }

        [Fact]
        public async Task Should_Deactivate_User_After_10_Failed_Login_Attemps()
        {
            SetArrangments();

            for (var i = 0; i < 10; i++)
            {
                try
                {
                    await _userService.LoginUserAsync("nickname", "falsepassword");
                }
                catch (Exception ex)
                {
                }
            }

            _userRepoMock.Verify(x => x.GetUserWithoutBetsAsync(It.IsAny<string>()), Times.Exactly(10));
            var userDTO = await _userRepoMock.Object.GetUserWithoutBetsAsync("nickname");
            Assert.Equal(UserStatus.NonActivated, userDTO.Status);
        }

        [Fact]
        public async Task Can_Login()
        {
            SetArrangments();

            var token = await this._userService.LoginUserAsync("nickname", "password");

            _userRepoMock.Verify(x => x.GetUserWithoutBetsAsync(It.IsAny<string>()), Times.Once);
            Assert.True(token != null);
        }

        [Fact]
        public async Task Login_Should_Throw_Exception_When_Nickname_Empty()
        {
            SetArrangments();

            await Assert.ThrowsAsync<Exception>(() => this._userService.LoginUserAsync(string.Empty, "password"));

            this._userRepoMock.Verify(x => x.GetUserWithoutBetsAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_No_User_With_Given_Nickname()
        {
            this.SetArrangments();

            await Assert.ThrowsAsync<Exception>(() =>
                this._userService.LoginUserAsync("thisnicknamedoesntexist", "password"));

            this._userRepoMock.Verify(x => x.GetUserWithoutBetsAsync("thisnicknamedoesntexist"), Times.Once);
        }

        [Fact]
        public async Task Can_Register_User()
        {
            SetArrangments();

            var userDto = new UserAuthDTO() {Email = "uniqueemail", NickName = "uniquenickname", Password = "password"};

            await this._userService.RegisterUserAsync(userDto);

            _userRepoMock.Verify(x => x.RegisterUserAsync(It.IsAny<User>()), Times.Once);
        }

        [Theory]
        [InlineData("email", "nickname", "")]
        [InlineData("", "nickname", "password")]
        [InlineData("email", "", "password")]
        public async Task Will_Throw_Exception_When_Any_Field_Empty(string email, string nickname, string password)
        {
            SetArrangments();
            var userAuth = new UserAuthDTO() {Email = email, NickName = nickname, Password = password};

            await Assert.ThrowsAsync<Exception>(() => this._userService.RegisterUserAsync(userAuth));
            _userRepoMock.Verify(x => x.RegisterUserAsync(It.IsAny<User>()), Times.Never);
        }


        [Theory]
        [InlineData("uniqueemail", "nickname")]
        [InlineData("email", "uniquenickname")]
        public async Task Will_Throw_Exception_When_User_Already_Exists(string email, string nickname)
        {
            SetArrangments();
            var user = new UserAuthDTO() {NickName = nickname, Email = email, Password = "password"};

            await Assert.ThrowsAsync<Exception>(() => this._userService.RegisterUserAsync(user));
            _userRepoMock.Verify(x => x.RegisterUserAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Can_Change_User_Status()
        {
            SetArrangments();

            await this._userService.ChangeUserStatusAsync("nickname", UserStatus.Banned);

            this._userRepoMock.Verify(x => x.ChangeUserStatusAsync("nickname", It.IsAny<UserStatus>()), Times.Once());
        }

        [Fact]
        public async Task Will_Change_Status_Throw_Exception_When_User_Doesnt_Exists()
        {
            SetArrangments();

            await Assert.ThrowsAsync<Exception>(() =>
                this._userService.ChangeUserStatusAsync("notexistentusernickname", UserStatus.Banned));
        }

        [Fact]
        public async Task Can_Get_Top_20_Users()
        {
            SetArrangments();

            var top = await this._userService.Get20BestUsersAsync();

            Assert.Equal(20, top.Count);

            _userRepoMock.Verify(x => x.Get20BestUsersAsync(), Times.Once);
        }


        private void SetArrangments()
        {
            Authorization.CreatePasswordHash("password", out var hash, out var salt);
            var userToReturn = new User
            {
                NickName = "nickname",
                PasswordHash = hash,
                Salt = salt,
                Status = UserStatus.Active,
                Email = "email"
            };

            var topList = MultiplyObject(userToReturn).ToList();

            _userRepoMock.Setup(x => x.GetUserWithoutBetsAsync("nickname")).ReturnsAsync(userToReturn);
            _userRepoMock.Setup(x => x.GetUserByEmailAsync("email")).ReturnsAsync(userToReturn);
            _userRepoMock.Setup(x => x.Get20BestUsersAsync())
                .ReturnsAsync(topList);


            _userService = new UserService(_userRepoMock.Object, _mapper, _options,
                _emailMock.Object);
        }

        private static IEnumerable<User> MultiplyObject(User user)
        {
            for (var i = 0; i < 20; i++)
            {
                yield return user;
            }
        }
    }
}