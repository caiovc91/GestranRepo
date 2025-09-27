using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByNameAndAccessHashAsync(string name, string accessHashCode, CancellationToken ct = default);
        public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}