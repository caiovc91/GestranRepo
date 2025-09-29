using Gestran.Backend.Application.Common.Helpers;
using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces.Services;
using Gestran.Backend.Application.Services;
using Gestran.Backend.Domain.Entities;
using Gestran.Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestran.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) { _userService = userService; }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var user = await _userService.ValidateLoginAsync(request.Name, request.Password);
            if (user == null || !user.IsAccessActive)
                return Unauthorized("Acesso inválido ou inativo");

            // Gera token
            var token = AuthHelper.GenerateFakeToken(user.Id, user.Role.ToString());

            // Retorna token e info básica do usuário
            var userDto = new UserDto(user.Id, user.Name, user.Role.ToString(), user.IsAccessActive);
            return Ok(new { Token = token, User = userDto });
        }

    }
}
