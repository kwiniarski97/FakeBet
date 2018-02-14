namespace FakeBet.Services
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using FakeBet.DTO;
    using FakeBet.Models;

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            this.CreateMap<User, UserDto>();

            this.CreateMap<UserRegisterDto, User>().AfterMap(
                (s, d) =>
                    {
                        d.CreateTime = DateTime.Now;
                        d.Status = UserStatus.NotActivated;
                        d.Points = 5000;
                        d.Salt = EncryptionService.GetSalt();
                        d.VotesHistory = new List<Vote>();
                    });
        }
    }
}