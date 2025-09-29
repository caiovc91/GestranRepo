using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces;
using Gestran.Backend.Domain.Entities;
using Gestran.Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.Services
{
    public class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _repo;
        private readonly ICheckListItemTypeRepository _itemTypeRepo;

        public CheckListService(ICheckListRepository repo, ICheckListItemTypeRepository itemTypeRepo) {
            _repo = repo;
            _itemTypeRepo = itemTypeRepo;
        }
        public async Task<IEnumerable<CheckListResponseDto>> GetAllCheckListsAsync(CancellationToken ct = default)
        {
            var list = await _repo.GetAllCheckListsAsync(ct);
            return list.Select(c => MapToResponseDto(c));
        }

        public async Task<CheckListResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var c = await _repo.GetCheckListByIdAsync(id, ct);
            if (c == null) return null;
            return MapToResponseDto(c);
        }

        public async Task<CheckListResponseDto> CreateAsync(CheckListCreateDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.CheckListName))
                throw new InvalidOperationException("CheckListName is required.");

            // Se collectionId for Guid.Empty ou null, tratamos como null
            Guid? collectionId = (dto.CollectionId == null || dto.CollectionId == Guid.Empty)
                ? null
                : dto.CollectionId;

            // 2️⃣ Criar a checklist
            var checklist = new CheckList
            {
                Id = Guid.NewGuid(),
                CheckListName = dto.CheckListName,
                ExecutedById = null,
                CollectionId = collectionId,
                CreationDate = DateTime.UtcNow,
                CurrentStatus = CheckListStatus.Created, // Status inicial
                InProgress = false,
                IsApproved = false,
                GeneralComments = dto.GeneralComments
            };

            // 3️⃣ Processar os checklist items
            checklist.CheckListItems = new List<CheckListItem>();

            foreach (var itemDto in dto.CheckListItems ?? Enumerable.Empty<CheckListItemCreateDto>())
            {
                if (itemDto.ItemTypeId == Guid.Empty)
                    throw new InvalidOperationException("ItemTypeId is required and must be a valid GUID.");

                

                // Busca o tipo no banco
                var type = await _itemTypeRepo.GetCheckListItemTypeAsync(itemDto.ItemTypeId, ct);
                if (type == null)
                    throw new InvalidOperationException($"ItemTypeId {itemDto.ItemTypeId} does not exist in the database.");

                checklist.CheckListItems.Add(new CheckListItem
                {
                    Id = Guid.NewGuid(),
                    CheckListId = checklist.Id,
                    ItemTypeId = itemDto.ItemTypeId,
                    ItemTypeName = type.TypeName,
                    IsChecked = itemDto.IsChecked ?? false,
                    Comments = itemDto.Comments ?? string.Empty
                });
            }

            // 4️⃣ Persistir no banco
            await _repo.AddNewCheckListAsync(checklist, ct);

            // 5️⃣ Retornar DTO para front-end
            return new CheckListResponseDto
            (
                Id: checklist.Id,
                CheckListName: checklist.CheckListName,
                ExecutedById: checklist.ExecutedById,
                CollectionId: checklist.CollectionId,
                ExecutedByName: null,
                CurrentStatus: checklist.CurrentStatus.ToString(),
                InProgress: checklist.InProgress,
                IsApproved: checklist.IsApproved,
                CreationDate: checklist.CreationDate,
                LastUpdateDate: checklist.LastUpdateDate,
                ApprovalDate: checklist.ApprovalDate,
                GeneralComments: checklist.GeneralComments,
                CheckListItems: checklist.CheckListItems.Select(i => new CheckListItemResponseDto(
                    Id: i.Id,
                    ItemTypeId: i.ItemTypeId,
                    ItemTypeName: i.ItemTypeName,
                    IsChecked: i.IsChecked,
                    Comments: i.Comments
                )).ToList()
            );
        }

        public async Task<bool> UpdateAsync(Guid id, CheckListUpdateDto dto, CancellationToken ct = default)
        {
            var existing = await _repo.GetCheckListByIdAsync(id, ct);
            if (existing == null) return false;

            existing.CheckListName = dto.CheckListName ?? existing.CheckListName;
            existing.GeneralComments = dto.GeneralComments ?? existing.GeneralComments;

            if(existing.CurrentStatus != CheckListStatus.Approved) // Não regrava se já aprovado
                existing.CurrentStatus = CheckListStatus.InProgress;

            existing.InProgress = dto.InProgress ?? existing.InProgress;

            if (dto.CheckListItems != null)
            {
                foreach (var itemDto in dto.CheckListItems)
                {
                    var item = existing.CheckListItems.FirstOrDefault(i => i.Id == itemDto.Id);
                    if (item != null)
                    {
                        item.IsChecked = itemDto.IsChecked ?? item.IsChecked;
                        item.Comments = itemDto.Comments ?? item.Comments;
                    }
                }
            }

            await _repo.UpdateCheckListAsync(existing, ct);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var existing = await _repo.GetCheckListByIdAsync(id, ct);
            if (existing == null) return false;
            await _repo.RemoveCheckListAsync(existing, ct);
            return true;
        }

        // Métodos auxiliares de execução
        public async Task<bool> StartExecutionAsync(Guid checklistId, Guid executorId, CancellationToken ct = default)
        {
            var checklist = await _repo.GetCheckListByIdAsync(checklistId, ct);
            if (checklist == null) return false;

            checklist.ExecutedById = executorId;
            checklist.InProgress = true;
            await _repo.UpdateCheckListAsync(checklist, ct);
            return true;
        }

        public async Task<bool> UpdateItemsAsync(Guid checklistId, Guid executorId, List<CheckListItemUpdateDto> items, CancellationToken ct = default)
        {
            var checklist = await _repo.GetCheckListByIdAsync(checklistId, ct);
            if (checklist == null || checklist.ExecutedById != executorId) return false;

            foreach (var dto in items)
            {
                var item = checklist.CheckListItems.FirstOrDefault(i => i.Id == dto.Id);
                if (item != null)
                {
                    item.IsChecked = dto.IsChecked ?? item.IsChecked;
                    item.Comments = dto.Comments ?? item.Comments;
                }
            }

            await _repo.UpdateCheckListAsync(checklist, ct);
            return true;
        }

        public async Task<bool> FinishExecutionAsync(Guid checklistId, Guid executorId, CancellationToken ct = default)
        {
            var checklist = await _repo.GetCheckListByIdAsync(checklistId, ct);
            if (checklist == null || checklist.ExecutedById != executorId) return false;

            checklist.InProgress = false;
            checklist.IsApproved = true;
            checklist.ApprovalDate = DateTime.UtcNow;
            await _repo.UpdateCheckListAsync(checklist, ct);
            return true;
        }

        public async Task<bool> AddCommentAsync(Guid checklistId, string comment, CancellationToken ct = default)
        {
            var checklist = await _repo.GetCheckListByIdAsync(checklistId, ct);
            if (checklist == null) return false;

            checklist.GeneralComments = comment;
            await _repo.UpdateCheckListAsync(checklist, ct);
            return true;
        }

        private CheckListResponseDto MapToResponseDto(CheckList c)
        {
            return new CheckListResponseDto(
                c.Id,
                c.CheckListName,
                c.ExecutedById,
                c.CollectionId,
                c.ExecutedBy?.Name,
                c.CurrentStatus.ToString(),
                c.InProgress,
                c.IsApproved,
                c.CreationDate,
                c.LastUpdateDate,
                c.ApprovalDate,
                c.GeneralComments,
                c.CheckListItems.Select(i => new CheckListItemResponseDto(
                    i.Id,
                    i.ItemTypeId,
                    i.ItemTypeName,
                    i.IsChecked ?? false,
                    i.Comments
                )).ToList()
            );
        }
    }
}
