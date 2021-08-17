using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Football.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly FootballContext _context;

        public GeneralController(FootballContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Nationalities")]
        public async Task<IActionResult> GetNationalities()
        {
            return Ok(await _context.Nationalities.ToListAsync());
        }
        [HttpGet]
        [Route("Countries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _context.Countries.ToListAsync());
        }
    }
}
