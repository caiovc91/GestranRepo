using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestran.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers(CancellationToken ct = default)
        {
            var users = await _userService.GetAllUsersAsync(ct);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto?>> GetUserById(Guid id, CancellationToken ct = default)
        {
            var user = await _userService.GetUserByIdAsync(id, ct);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
