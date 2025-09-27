using Gestran.Backend.Application.Common.Helpers;
using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.Handlers
{
    public class AuthHandler
    {
        private readonly IUserRepository _userRepo;
        public AuthHandler(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
        {
            var user = await _userRepo.GetUserByNameAndAccessHashAsync(request.Name, request.Password, ct);
            if (user == null || !user.IsAccessActive) return null;

            if (!AuthHelper.VerifyHash(request.Password, user.AccessHashCode)) return null;

            var token = AuthHelper.GenerateFakeToken(user.Id, user.Role.ToString());
            return new LoginResponseDto(user.Id, token, new UserDto(user.Id, user.Name, user.Role.ToString(), user.IsAccessActive));
        }
    }
}


