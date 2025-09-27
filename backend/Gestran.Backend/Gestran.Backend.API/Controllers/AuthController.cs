using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Common.Helpers;
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
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context) => _context = context;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == loginDto.Name);

            if (user == null || !AuthHelper.VerifyHash(loginDto.Password, user.AccessHashCode) || !user.IsAccessActive)
                return Unauthorized(new { message = "Login inválido ou desativado." });

            var token = AuthHelper.GenerateFakeToken(user.Id, user.Role.ToString());

            var userDto = new UserDto(user.Id, user.Name, user.Role.ToString(), user.IsAccessActive);

            return Ok(new { user = userDto, token });
        }

    }
}
