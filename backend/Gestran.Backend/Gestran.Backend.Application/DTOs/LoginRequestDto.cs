using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.DTOs
{
    public record LoginRequestDto(
        string Name,
        string Password
    );
}
