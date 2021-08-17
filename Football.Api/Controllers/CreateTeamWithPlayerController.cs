using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Data;
using Football.Api.Dtos;
using Football.Api.Helpers;
using Football.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Football.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateTeamWithPlayerController : ControllerBase
    {
        private readonly FootballContext _context;
        private readonly IHostEnvironment _environment;

        public CreateTeamWithPlayerController(FootballContext context,IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateTeamWithPlayersDto dto)
        {
            //return Ok();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var team = new Team()
            {
                Name = dto.Name,
                CoachName = dto.CoachName,
                CountryId = dto.CountryId,
                FoundationDate = dto.FoundationDate
            };

            var logoUrl = string.Empty;
            if (dto.Logo != null)
            {
                logoUrl = await FileHelper.SaveImageOrLogo(_environment,dto.Logo,"logo");
            }
            team.LogoUrl = logoUrl;


            var playerObjects = JsonConvert.DeserializeObject<CreatePlayerDto[]>(dto.Players);

            var players = new List<Player>();
            var i = 0;
            foreach (var player in playerObjects)
            {
                var newPlayer = new Player()
                {
                    Name = player.Name,
                    NationalityId = player.NationalityId,
                    DateOfBirth = player.DateOfBirth
                };

                var imageUrl = string.Empty;
                if (dto.PlayerImages[i] != null)
                {
                    imageUrl = await FileHelper.SaveImageOrLogo(_environment,dto.PlayerImages[i],"img");
                }
                newPlayer.ImageUrl = imageUrl;
                players.Add(newPlayer);
                i += 1;

            }

            team.Players = players;

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return Ok();
        }

        
    }
}
