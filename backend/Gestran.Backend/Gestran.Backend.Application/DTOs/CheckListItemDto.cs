using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record CheckListItemDto(
        Guid Id,
        Guid ItemTypeId,
        string ItemTypeName,
        bool? IsChecked,
        string? Comments
    );

}
