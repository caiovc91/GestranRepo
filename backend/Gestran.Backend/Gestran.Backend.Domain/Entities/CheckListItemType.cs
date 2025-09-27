using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Entities
{
    public class CheckListItemType
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }

        //navegação reversa
        public ICollection<CheckListItem> CheckListItems { get; set; } = new List<CheckListItem>();
    }
}
