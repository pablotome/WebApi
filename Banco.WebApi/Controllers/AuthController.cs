using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco.WebApi.DTOs.Requests;
using Banco.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutorizacionService _autorizacionService;
        public AuthController(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var jwtToken = await _autorizacionService.Login(loginDTO);
            if (jwtToken == null)
                return Unauthorized();

            return Ok(jwtToken);
        }
    }
}