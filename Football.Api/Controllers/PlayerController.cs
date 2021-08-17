using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Football.Api.Data;
using Football.Api.Dtos;
using Football.Api.Helpers;
using Football.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Football.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,Normal")]

    public class PlayerController : ControllerBase
    {
        private readonly FootballContext _context;
        private readonly IHostEnvironment _environment;
        private readonly IMapper _mapper;

        public PlayerController(FootballContext context,IHostEnvironment environment,IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }

        [Route("team/{teamId}")]
        [HttpGet]
        public async Task<IActionResult> Players(int teamId)
        {
            var players = await _context.Players
                .Include(p=>p.Nationality)
                .Include(p=>p.Team)
                .Where(p=>p.TeamId == teamId)
                .ToListAsync();

            var result = _mapper.Map<IList<Player>, IList<PlayerDto>>(players);
            return Ok(result);
        }

        [HttpGet]
        [Route("{playerId}",Name = "GetPlayer")]
        public async Task<IActionResult> Get(int playerId)
        {
            var player = await _context.Players
                .Include(p=>p.Team)
                .Include(p=>p.Nationality)
                .FirstOrDefaultAsync(p => p.Id == playerId);

            if (player is null)
                return NotFound();

            return Ok(_mapper.Map<Player,PlayerDto>(player));
        }

        [HttpPut]
        [Route("Edit")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Edit([FromForm]EditPlayerDto player)
        {
            var playerInDb = await _context.Players
                .Include(p=>p.Nationality)
                .Include(p=>p.Team)
                .FirstOrDefaultAsync(p => p.Id == player.Id);

            if (playerInDb is null)
                return NotFound();

            var imagePath = playerInDb.ImageUrl;
            if (player.Image != null)
            {
                imagePath =await FileHelper.SaveImageOrLogo(_environment,player.Image,"img");
            }

            _mapper.Map<EditPlayerDto, Player>(player, playerInDb);

            playerInDb.ImageUrl = imagePath;
          

            _context.Entry(playerInDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var newPlayer = await _context.Players
                .Include(p => p.Team)
                .Include(p => p.Nationality)
                .FirstOrDefaultAsync(p => p.Id == player.Id);
            return Ok(_mapper.Map<Player, PlayerDto>(newPlayer));
        }

        [HttpDelete]
        [Route("{playerId}")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<IActionResult> Delete(int playerId)
        {
            var playerInDb = await _context.Players
                .FirstOrDefaultAsync(p => p.Id == playerId);

            if (playerInDb is null)
                return NotFound();

            _context.Players.Remove(playerInDb);
            await _context.SaveChangesAsync();
            return Ok(playerInDb);
        }


    }
}
