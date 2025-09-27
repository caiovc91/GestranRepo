using Gestran.Backend.Application.Interfaces;
using Gestran.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Infrastructure.Persistence.Repositories
{
    public class CheckListRepository : ICheckListRepository
    {
        private readonly AppDbContext _context;

        public CheckListRepository(AppDbContext context) => _context = context;



        public async Task AddNewCheckListAsync(CheckList checklist, CancellationToken ct = default)
        {
            _context.CheckLists.Add(checklist);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<CheckList>> GetCheckListByDateAsync(DateTime date, CancellationToken ct = default)
        {
            return await _context.CheckLists.Include(c => c.CheckListItems)
                        .ThenInclude(i => i.ItemType)
                        .Where(c => c.CreationDate == date.Date)
                        .ToListAsync();

        }

        public async Task<CheckList?> GetCheckListByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.CheckLists
                        .Include(c => c.CheckListItems)
                        .ThenInclude(i => i.ItemType)
                        .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task UpdateChecklistAsync(CheckList checklist, CancellationToken ct = default)
        {
            _context.CheckLists.Update(checklist);
            await _context.SaveChangesAsync(ct);
        }
    }
}
