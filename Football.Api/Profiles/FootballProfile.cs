using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Football.Api.Dtos;
using Football.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Football.Api.Profiles
{
    public class FootballProfile :Profile
    {
        private readonly string _baseUrl = "https://localhost:44351/";
        public FootballProfile()
        {
            CreateMap<Player, PlayerDto>()
                .ForMember(des=>des.ImageUrl,
                    opt=>opt.MapFrom(src=>_baseUrl + src.ImageUrl));

            CreateMap<EditPlayerDto, Player>();

            CreateMap<Team, TeamDto>()
                .ForMember(des => des.LogoUrl,
                    opt => opt.MapFrom(src => _baseUrl + src.LogoUrl));

            CreateMap<EditTeamDto, Team>();
        }
    }
}
