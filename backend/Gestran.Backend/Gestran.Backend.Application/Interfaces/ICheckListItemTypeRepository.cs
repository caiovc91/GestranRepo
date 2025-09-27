using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Interfaces
{
    public interface ICheckListItemTypeRepository
    {
        Task AddNewCheckListItemTypeAsync(CheckListItemType checkListItemType, CancellationToken ct = default);
        Task<CheckListItemType?> GetCheckListItemTypeAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<CheckListItemType>> GetAllCheckListItemTypesAsync(CancellationToken ct = default);
        Task RemoveCheckListItemTypeAsync(Guid id, CancellationToken ct = default);
    }
}