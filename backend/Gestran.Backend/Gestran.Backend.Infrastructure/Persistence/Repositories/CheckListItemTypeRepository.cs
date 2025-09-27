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
    public class CheckListItemTypeRepository : ICheckListItemTypeRepository
    {
        private readonly AppDbContext _context;

        public CheckListItemTypeRepository(AppDbContext context) => _context = context;

        public async Task AddNewCheckListItemTypeAsync(CheckListItemType checkListItemType, CancellationToken ct = default)
        {
            if (_context.CheckListItemTypes.Any(c => c.TypeName == checkListItemType.TypeName))
                throw new InvalidOperationException($"CheckListItemType with Id {checkListItemType.Id} already exists.");

            _context.CheckListItemTypes.Add(checkListItemType);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<CheckListItemType>> GetAllCheckListItemTypesAsync(CancellationToken ct = default)
        {
           return await _context.CheckListItemTypes.Where(c => c.IsEnabled).ToListAsync();
        }

        public async Task<CheckListItemType?> GetCheckListItemTypeAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.CheckListItemTypes.FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        /// <summary>
        /// Remoção não deleta de fato, apenas desabilita o item para não ser incluido em futuros checklists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task RemoveCheckListItemTypeAsync(Guid id, CancellationToken ct = default)
        {
            var itemtype = await _context.CheckListItemTypes.FindAsync([id], ct);
            if (itemtype is null)
                throw new KeyNotFoundException($"Check list item type with Id {id} not found.");

            itemtype.IsEnabled = false;
            _context.CheckListItemTypes.Update(itemtype);
            await _context.SaveChangesAsync(ct);
        }
    }
}
