using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken ct = default);
        Task<User?> ValidateLoginAsync(string name, string password, CancellationToken ct = default);
        Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
    }
}