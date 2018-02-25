using System;
using System.Collections.Generic;
using AutoMapper;
using FakeBet.DTO;
using FakeBet.Extensions;
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
            CreateMap<MatchDTO, Match>();
            CreateMap<Match, MatchDTO>();
        }

        private void CreateVoteMaps()
        {
            CreateMap<VoteDTO, Vote>();
            CreateMap<Vote, VoteDTO>();
        }
    }
}