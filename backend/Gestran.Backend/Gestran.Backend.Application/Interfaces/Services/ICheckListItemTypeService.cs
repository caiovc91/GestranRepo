using Gestran.Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.Interfaces.Services
{
    public interface ICheckListItemTypeService
    {
        Task<IEnumerable<CheckListItemTypeResponseDto>> GetAllAsync(CancellationToken ct = default);
        Task<CheckListItemTypeResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<CheckListItemTypeResponseDto> CreateAsync(CheckListItemTypeCreateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
