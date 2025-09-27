using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Entities
{
    public class CheckListItem
    {
        public Guid Id { get; set; }
        public Guid CheckListId { get; set; }
        public Guid ItemTypeId { get; set; }
        public CheckListItemType? ItemType { get; set; } = null;
        public bool? IsChecked { get; set; }
        public string? Comments { get; set; }

        //navegação reversa
        public CheckList? CheckList { get; set; }
    }
}
