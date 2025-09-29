using Gestran.Backend.Application.Common.Helpers;
using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces;
using Gestran.Backend.Application.Interfaces.Services;
using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken ct = default)
        {
            var users = await _userRepository.GetAllUsersAsync(ct);
            return users.Select(u => new UserDto(u.Id, u.Name, u.Role.ToString(), u.IsAccessActive));
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(id, ct);
            return user == null ? null : new UserDto(user.Id, user.Name, user.Role.ToString(), user.IsAccessActive);
        }

        public async Task<User?> ValidateLoginAsync(string name, string password, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByNameAsync(name, ct);

            if (user == null)
                return null;

            if (!AuthHelper.VerifyHash(password, user.AccessHashCode))
                return null;

            // Retornar usuário caso válido
            return user;
        }
    }
}
