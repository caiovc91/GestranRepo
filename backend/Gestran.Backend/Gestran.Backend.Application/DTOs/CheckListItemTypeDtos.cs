using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record CheckListItemTypeCreateDto(string TypeName, string? Description, bool IsEnabled);
    public record CheckListItemTypeUpdateDto(Guid Id, string TypeName, string? Description, bool IsEnabled);
    public record CheckListItemTypeResponseDto(Guid Id, string TypeName, string? Description, bool IsEnabled);
}
