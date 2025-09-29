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

        public async Task<IEnumerable<CheckList>> GetAllCheckListsAsync(CancellationToken ct = default)
        {
            return await _context.CheckLists
                .Include(cl => cl.CheckListItems)
                .ThenInclude(i => i.ItemTypeName)
                .Include(cl => cl.ExecutedBy)
                .Include(cl => cl.Collection)
                .ToListAsync();
        }

        public async Task AddNewCheckListAsync(CheckList checklist, CancellationToken ct = default)
        {
            await _context.CheckLists.AddAsync(checklist);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<CheckList>> GetCheckListByDateAsync(DateTime date, CancellationToken ct = default)
        {
            return await _context.CheckLists.Include(c => c.CheckListItems)
                        .ThenInclude(i => i.ItemTypeName)
                        .Where(c => c.CreationDate == date.Date)
                        .ToListAsync();

        }

        public async Task<CheckList?> GetCheckListByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.CheckLists
                        .Include(c => c.CheckListItems)
                        .ThenInclude(i => i.ItemTypeName)
                        .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task UpdateCheckListAsync(CheckList checklist, CancellationToken ct = default)
        {
            _context.CheckLists.Update(checklist);
            await _context.SaveChangesAsync(ct);
        }

        /// <summary>
        /// retorna todas as checklists (inclusive já aprovadas/fechadas)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CheckList>> GetAllCheckListsHistoryAsync(CancellationToken ct = default)
        {
            return await _context.CheckLists
                .AsNoTracking()
                .Include(cl => cl.CheckListItems)
                .ThenInclude(i => i.ItemTypeName)
                .Include(cl => cl.ExecutedBy)
                .Include(cl => cl.Collection)
                .OrderByDescending(cl => cl.CreationDate)
                .ToListAsync();
        }

        public async Task RemoveCheckListAsync(CheckList checkListEntity, CancellationToken ct = default)
        {
            _context.CheckLists.Remove(checkListEntity);
            await _context.SaveChangesAsync();
        }
    }
}
