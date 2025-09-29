using Gestran.Backend.Domain.Entities;
using Gestran.Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Application.Common.Helpers
{
    public static class CheckListStatusHelper
    {
        public static CheckListStatus EvaluateStatus(CheckList checklist)
        {
            if (checklist.CheckListItems == null || checklist.CheckListItems.Count == 0)
                return CheckListStatus.Created; 

            int totalItems = checklist.CheckListItems.Count;
            int checkedItems = checklist.CheckListItems.Count(i => i.IsChecked == true);

            if (checkedItems == 0)
                return CheckListStatus.Created;

            if (checkedItems < totalItems)
                return CheckListStatus.InProgress;

            return CheckListStatus.Finished; // todos marcados
        }
    }
}
