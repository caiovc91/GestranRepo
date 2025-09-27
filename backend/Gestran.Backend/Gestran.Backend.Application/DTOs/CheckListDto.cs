using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record CheckListDto(
            Guid? Id,
            string CheckListName,
            Guid? ExecutedById,
            string? ExecutedByName,
            string? CurrentStatus,
            bool IsInProgress,
            bool IsApproved,
            DateTime? CreatedAt,
            DateTime? UpdatedAt,
            DateTime? ApprovedAt,
            List<CheckListItemDto> CheckListItems
        );

}
