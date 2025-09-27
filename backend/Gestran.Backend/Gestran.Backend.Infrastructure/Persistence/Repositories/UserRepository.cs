using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gestran.Backend.Application.Interfaces;
using Gestran.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gestran.Backend.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) =>  _context = context;

        /// <summary>
        /// Busca usuário por nome e hash (senha) e verifica se o acesso está ativo
        /// </summary>
        /// <param name="name"></param>
        /// <param name="accessHashCode"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByNameAndAccessHashAsync(string name, string accessHashCode, CancellationToken ct = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.Name == name && u.AccessHashCode == accessHashCode && u.IsAccessActive, ct);

        /// <summary>
        /// Busca usuário por ID apenas.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _context.Users.FindAsync([id], ct);
    }
}
