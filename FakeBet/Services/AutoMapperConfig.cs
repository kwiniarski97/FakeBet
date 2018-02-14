namespace FakeBet.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using AutoMapper;

    using FakeBet.DTO;
    using FakeBet.Models;
    using FakeBet.Services.Interfaces;

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            this.CreateUserMaps();

            this.CreateMatchMaps();
        }

        private void CreateUserMaps()
        {
            this.CreateMap<User, UserDTO>();

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

        private void CreateMatchMaps()
        {
            this.CreateMap<MatchAddDTO, Match>().AfterMap(
                (s, d) =>
                    {
                        d.Status = s.Status ?? DetermineMatchStatus(s);
                        d.MatchId = s.TeamAName + s.TeamBName + s.Category + s.MatchTime.Ticks;
                        d.TeamAPoints = 0;
                        d.TeamBPoints = 0;
                        d.PointsRatio = 1;
                        d.Votes = new List<Vote>();
                    });
        }

        private static MatchStatus DetermineMatchStatus(MatchAddDTO matchAdd)
        {
            return DateTime.Compare(matchAdd.MatchTime, DateTime.Now) > 0 ? MatchStatus.NonStarted : MatchStatus.Ended;
        }
    }
}