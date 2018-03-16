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

            var options = new AppSettingsSecret() { Secret = "secretmustbelongerthat128bits" };
            _options = Options.Create(options);
        }

        [Fact]
        public async Task GetUserByNickName()
        {
            SetGetUserMethodArragments();

            var user = await _userService.GetUserAsync("nickname");

            Assert.Equal("nickname", user.NickName);
        }

        [Fact]
        public async Task Get_User_Should_Return_Null_When_User_Doesnt_Exist()
        {
            SetGetUserMethodArragments();

            var user = await _userService.GetUserAsync("non existent user id");

            _userRepoMock.Verify(x => x.GetUserAsync(It.IsAny<string>()), Times.Once);
            Assert.Null(user);
        }

        [Fact]
        public async Task Should_Deactivate_User_After_10_Failed_Login_Attemps()
        {
            SetLoginMethodArrangments();

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
            SetLoginMethodArrangments();

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
            SetLoginMethodArrangments();

            var user = await this._userService.LoginUserAsync(string.Empty, "password");

            this._userRepoMock.Verify(x => x.GetUserAsync(It.IsAny<string>()), Times.Never);
            Assert.Null(user);
        }

        [Fact]
        public async Task Should_Return_Null_When_No_User_With_Given_Nickname()
        {
            this.SetLoginMethodArrangments();

            var user = await this._userService.LoginUserAsync("thisnicknamedoesntexist", "password");

            this._userRepoMock.Verify(x => x.GetUserAsync("thisnicknamedoesntexist"), Times.Once);
            Assert.Null(user);
        }

        private void SetGetUserMethodArragments()
        {
            _userRepoMock.Setup(x => x.GetUserAsync("nickname")).ReturnsAsync(new User { NickName = "nickname" });
            _userService = new UserService(_userRepoMock.Object, _mapper, _options);
        }

        private void SetLoginMethodArrangments()
        {
            Authorization.CreatePasswordHash("password", out var hash, out var salt);
            _userRepoMock.Setup(x => x.GetUserAsync("nickname")).ReturnsAsync(
                new User { NickName = "nickname", PasswordHash = hash, Salt = salt, Status = UserStatus.Active });
            _userService = new UserService(_userRepoMock.Object, _mapper, _options);
        }
    }
}