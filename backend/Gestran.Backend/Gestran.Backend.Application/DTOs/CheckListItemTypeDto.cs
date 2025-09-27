using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record CheckListItemTypeDto(
            Guid? Id,
            string TypeName,
            string? Description,
            bool isEnabled
        );
}
