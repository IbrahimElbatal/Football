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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Normal")]

    public class TeamController : ControllerBase
    {
        private readonly FootballContext _context;
        private readonly IHostEnvironment _environment;
        private readonly IMapper _mapper;

        public TeamController(FootballContext context,IHostEnvironment environment ,IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("teams")]
        public async Task<IActionResult> Teams()
        {
            var teams = await _context.Teams
                .Include(t=>t.Country)
                .ToListAsync();

            var result = _mapper.Map<IList<Team>, IList<TeamDto>>(teams);
            return Ok(result);
        }

        [HttpGet]
        [Route("{teamId}",Name = "GetTeam")]
        public async Task<IActionResult> Get(int teamId)
        {
            var team = await _context.Teams
                .Include(t=>t.Country)
                .FirstOrDefaultAsync(p => p.Id == teamId);

            if (team is null)
                return NotFound();

            return Ok(_mapper.Map<Team,TeamDto>(team));
        }

        [HttpPut]
        [Route("Edit")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<IActionResult> Edit([FromForm]EditTeamDto team)
        {
            var teamInDb = await _context.Teams
                .FirstOrDefaultAsync(p => p.Id == team.Id);

            if (teamInDb is null)
                return NotFound();

            var logoUrl = teamInDb.LogoUrl;
            if (team.Logo != null)
            {
                logoUrl =await FileHelper.SaveImageOrLogo(_environment,team.Logo,"logo");
            }

            _mapper.Map<EditTeamDto, Team>(team, teamInDb);
            teamInDb.LogoUrl = logoUrl;

            _context.Entry(teamInDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            var newTeam = await _context.Teams
                .Include(t => t.Country)
                .FirstOrDefaultAsync(p => p.Id == teamInDb.Id);

            return Ok(_mapper.Map<Team, TeamDto>(newTeam));

        }

        [HttpDelete]
        [Route("{teamId}")]
        [AllowAnonymous]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Delete(int teamId)
        {
            var teamInDb = await _context.Teams
                .FirstOrDefaultAsync(p => p.Id == teamId);

            if (teamInDb is null)
                return NotFound();

            _context.Teams.Remove(teamInDb);
            await _context.SaveChangesAsync();
            return Ok(teamInDb);
        }
    }
}
