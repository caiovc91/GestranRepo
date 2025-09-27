using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record LoginResponseDto(
        Guid id,
        string Token,
        UserDto User
    );
}
