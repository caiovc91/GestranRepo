using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Enums
{
    public enum CheckListStatus
    {
        InProgress = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum UserRole
    {
        Executor = 0,
        Supervisor = 1,
    }
}
