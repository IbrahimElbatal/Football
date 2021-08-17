using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Dtos;
using Football.Api.Helpers;
using Football.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Football.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISetting _setting;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthController(ISetting setting,UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _setting = setting;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                return NotFound();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                return BadRequest("Email and password are incorrect");

            var roles = await _userManager.GetRolesAsync(user);
            var token = TokenHelper.CreateToken(user,roles.FirstOrDefault(), _setting);
            return Ok(new {Token= token});
        }


    }
}
