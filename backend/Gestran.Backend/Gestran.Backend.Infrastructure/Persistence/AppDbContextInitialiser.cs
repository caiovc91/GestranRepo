using Gestran.Backend.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Infrastructure.Persistence
{
    public class AppDbContextInitialiser
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    var created = await _context.Database.EnsureCreatedAsync();
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Database created: {Created}", created);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        /// <summary>
        /// Seed para testes, implementa alguns itens de checklist e usuários para teste, executores e aprovadores
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            try
            {
                if (!await _context.Users.AnyAsync())
                {
                    var users = SeedDataHelper.GenerateTestUsers();
                    _context.Users.AddRange(users);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Usuários criados.");
                }

                if (!await _context.CheckListItemTypes.AnyAsync())
                {
                    var itemTypes = SeedDataHelper.GenerateTestCheckListItemTypes();
                    _context.CheckListItemTypes.AddRange(itemTypes);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Criados itens de checklist padrões.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inicializar o banco.");
                throw;
            }
        }

    }
}
