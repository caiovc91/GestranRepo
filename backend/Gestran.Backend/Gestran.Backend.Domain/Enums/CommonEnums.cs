using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Enums
{
    public enum CheckListStatus
    {
        Created = 0,
        InProgress = 1,
        Finished = 2,
        Approved = 3,
        Rejected = 4
    }

    public enum UserRole
    {
        Executor = 0,
        Supervisor = 1,
    }
}
