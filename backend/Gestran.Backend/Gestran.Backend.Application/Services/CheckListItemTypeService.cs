using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces;
using Gestran.Backend.Application.Interfaces.Services;
using Gestran.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.Services
{
    public class CheckListItemTypeService : ICheckListItemTypeService
    {
        private readonly ICheckListItemTypeRepository _repository;

        public CheckListItemTypeService(ICheckListItemTypeRepository repository) => _repository = repository;

        public async Task<IEnumerable<CheckListItemTypeResponseDto>> GetAllAsync(CancellationToken ct = default)
        {
            var types = await _repository.GetAllCheckListItemTypesAsync(ct);
            return types.Select(t => new CheckListItemTypeResponseDto(
                t.Id,
                t.TypeName,
                t.Description,
                t.IsEnabled
            ));
        }

        public async Task<CheckListItemTypeResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var type = await _repository.GetCheckListItemTypeAsync(id, ct);
            if (type == null) return null;

            return new CheckListItemTypeResponseDto(
                type.Id,
                type.TypeName,
                type.Description,
                type.IsEnabled
            );
        }

        public async Task<CheckListItemTypeResponseDto> CreateAsync(CheckListItemTypeCreateDto dto, CancellationToken ct = default)
        {
            var entity = new CheckListItemType
            {
                Id = Guid.NewGuid(),
                TypeName = dto.TypeName,
                Description = dto.Description,
                IsEnabled = true
            };

            await _repository.AddNewCheckListItemTypeAsync(entity, ct);

            return new CheckListItemTypeResponseDto(
                entity.Id,
                entity.TypeName,
                entity.Description,
                entity.IsEnabled
            );
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var existing = await _repository.GetCheckListItemTypeAsync(id, ct);
            if (existing == null) return false;

            await _repository.RemoveCheckListItemTypeAsync(existing.Id, ct);
            return true;
        }
    }
}
