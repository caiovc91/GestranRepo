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
    public class CheckListCollectionRepository : ICheckListCollectionRepository
    {
        private readonly AppDbContext _context;
        public CheckListCollectionRepository(AppDbContext context) => _context = context;
        
        public async Task AddOrUpdateCollectionAsync(CheckListCollection collection, CancellationToken ct = default)
        {
            var existingCollection = await _context.CheckListCollections
                .FindAsync([collection.CollectionId], ct);

            if(existingCollection == null)
                _context.CheckListCollections.Add(collection);
            else
                _context.Entry(existingCollection).CurrentValues.SetValues(collection);

            await _context.SaveChangesAsync(ct);
        }

        public async Task<CheckListCollection?> GetCollectionByOwnerId(Guid ownerId, CancellationToken ct = default)
        {
           return await _context.CheckListCollections.Include(c => c.CheckLists)
                .ThenInclude(cl => cl.CheckListItems)
                .ThenInclude(cli => cli.ItemTypeName)
                .FirstOrDefaultAsync(c => c.OwnerId == ownerId, ct);
        }

        public async Task RemoveCheckListFromCollectionAsync(Guid checkListId, CancellationToken ct = default)
        {
            var collection = await _context.CheckListCollections
                .Include(c => c.CheckLists).ToListAsync();

            foreach (var col in collection)
            {
                var colItem = col.CheckLists.FirstOrDefault(cl => cl.Id == checkListId);
                if (colItem != null)
                    col.CheckLists.Remove(colItem);

                await _context.SaveChangesAsync(ct);
            }
        }
    }
}
