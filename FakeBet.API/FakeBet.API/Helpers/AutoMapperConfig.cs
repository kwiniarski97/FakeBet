using AutoMapper;
using FakeBet.API.DTO;
using FakeBet.API.Models;

namespace FakeBet.API.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateUserMaps();

            CreateMatchMaps();

            CreateBetMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserAuthDTO, User>();

            CreateMap<User, UserTopDTO>();

            CreateMap<UserDTO, User>();
        }

        private void CreateMatchMaps()
        {
            CreateMap<MatchDTO, Match>();
            CreateMap<Match, MatchDTO>();
        }

        private void CreateBetMaps()
        {
            CreateMap<BetDTO, Bet>();
            CreateMap<Bet, BetDTO>();
        }
    }
}