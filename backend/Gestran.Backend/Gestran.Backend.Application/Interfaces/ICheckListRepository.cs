using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Interfaces
{
    public interface ICheckListRepository
    {
        public Task<CheckList?> GetCheckListByIdAsync(Guid id, CancellationToken ct = default);
        public Task AddNewCheckListAsync(CheckList checklist, CancellationToken ct = default);
        public Task UpdateChecklistAsync(CheckList checklist, CancellationToken ct = default);
        public Task<IEnumerable<CheckList>> GetCheckListByDateAsync(DateTime date, CancellationToken ct = default);
    }
}