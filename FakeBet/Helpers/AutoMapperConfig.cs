using System;
using System.Collections.Generic;
using AutoMapper;
using FakeBet.DTO;
using FakeBet.Models;

namespace FakeBet.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateUserMaps();

            CreateMatchMaps();

            CreateVoteMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserAuthDto, User>();

            CreateMap<User, UserTopDTO>();
        }

        private void CreateMatchMaps()
        {
            CreateMap<MatchAddDTO, Match>().AfterMap(
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

        private void CreateVoteMaps()
        {
            CreateMap<VoteAddDTO, Vote>().AfterMap((s, r) => { r.VoteId = new Guid(); });
        }

        private static MatchStatus DetermineMatchStatus(MatchAddDTO matchAdd)
        {
            return DateTime.Compare(matchAdd.MatchTime, DateTime.Now) > 0 ? MatchStatus.NonStarted : MatchStatus.Ended;
        }
    }
}