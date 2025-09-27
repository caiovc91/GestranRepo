using Gestran.Backend.Domain.Entities;

namespace Gestran.Backend.Application.Interfaces
{
    public interface ICheckListCollectionRepository
    {
        Task<CheckListCollection?> GetCollectionByOwnerId(Guid ownerId, CancellationToken ct = default);
        Task AddOrUpdateCollectionAsync(CheckListCollection collection, CancellationToken ct = default);
        Task RemoveCheckListFromCollectionAsync(Guid checkListId, CancellationToken ct = default);
    }
}