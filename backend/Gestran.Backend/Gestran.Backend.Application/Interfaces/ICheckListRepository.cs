using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Interfaces
{
    public interface ICheckListRepository
    {
        Task<IEnumerable<CheckList>> GetAllCheckListsAsync(CancellationToken ct = default);
        Task<CheckList?> GetCheckListByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<CheckList>> GetCheckListByDateAsync(DateTime date, CancellationToken ct = default);
        Task AddNewCheckListAsync(CheckList checkListEntity, CancellationToken ct = default);
        Task UpdateCheckListAsync(CheckList checkListEntity, CancellationToken ct = default);
        Task RemoveCheckListAsync(CheckList checkListEntity, CancellationToken ct = default);


        Task<IEnumerable<CheckList>> GetAllCheckListsHistoryAsync(CancellationToken ct = default);
    }
}