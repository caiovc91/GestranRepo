using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Domain.Entities;


namespace Gestran.Backend.Application.Services
{
    public interface ICheckListService
    {
        Task<IEnumerable<CheckListResponseDto>> GetAllCheckListsAsync(CancellationToken ct = default);
        Task<CheckListResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<CheckListResponseDto> CreateAsync(CheckListCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, CheckListUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);

        // Execution flows
        Task<bool> StartExecutionAsync(Guid checklistId, Guid executorId, CancellationToken ct = default);
        Task<bool> UpdateItemsAsync(Guid checklistId, Guid executorId, List<CheckListItemUpdateDto> items, CancellationToken ct = default);
        Task<bool> FinishExecutionAsync(Guid checklistId, Guid executorId, CancellationToken ct = default);
        Task<bool> AddCommentAsync(Guid checklistId, string comment, CancellationToken ct = default);

    }
}