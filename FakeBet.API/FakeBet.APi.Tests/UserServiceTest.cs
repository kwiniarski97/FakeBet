using System;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.Helpers;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Implementations;
using FakeBet.API.Services.Interfaces;
using Microsoft.Extensions.Options;
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

        public UserServiceTest()
        {
            var autoMapperProfile = new AutoMapperConfig();
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(autoMapperProfile); });
            _mapper = config.CreateMapper();

            _userRepoMock = new Mock<IUserRepository>();

            var options = new AppSettingsSecret() {Secret = "secretmustbelongerthat128bits"};
            _options = Options.Create(options);
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

            _userRepoMock.Verify(x => x.GetUserAsync(It.IsAny<string>()), Times.Once);
            Assert.Null(user);
        }

        [Fact]
        public async Task Should_Deactivate_User_After_10_Failed_Login_Attemps()
        {
            SetArrangments();

            for (var i = 0; i < 10; i++)
            {
                await _userService.LoginUserAsync("nickname", "falsepassword");
            }

            _userRepoMock.Verify(x => x.GetUserAsync(It.IsAny<string>()), Times.Exactly(10));
            var userDTO = await _userRepoMock.Object.GetUserAsync("nickname");
            Assert.Equal(UserStatus.NotActivated, userDTO.Status);
        }

        [Fact]
        public async Task Can_Login()
        {
            SetArrangments();

            var user = await this._userService.LoginUserAsync("nickname", "password");

            _userRepoMock.Verify(x => x.GetUserAsync(It.IsAny<string>()), Times.Once);
            Assert.True(user != null);
            Assert.IsType<UserDTO>(user);
            Assert.Equal("nickname", user.NickName);
            Assert.True(user.Token != null);
        }

        [Fact]
        public async Task Login_Should_Return_Null_When_Nickname_Empty()
        {
            SetArrangments();

            var user = await this._userService.LoginUserAsync(string.Empty, "password");

            this._userRepoMock.Verify(x => x.GetUserAsync(It.IsAny<string>()), Times.Never);
            Assert.Null(user);
        }

        [Fact]
        public async Task Should_Return_Null_When_No_User_With_Given_Nickname()
        {
            this.SetArrangments();

            var user = await this._userService.LoginUserAsync("thisnicknamedoesntexist", "password");

            this._userRepoMock.Verify(x => x.GetUserAsync("thisnicknamedoesntexist"), Times.Once);
            Assert.Null(user);
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
            _userRepoMock.Setup(x => x.GetUserAsync("nickname")).ReturnsAsync(userToReturn);
            _userRepoMock.Setup(x => x.GetUserByEmailAsync("email")).ReturnsAsync(userToReturn);


            _userService = new UserService(_userRepoMock.Object, _mapper, _options);
        }
    }
}