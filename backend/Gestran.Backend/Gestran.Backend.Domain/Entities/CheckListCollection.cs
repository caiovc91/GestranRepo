using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Entities
{
    public class CheckListCollection
    {
        public Guid CollectionId { get; set; }
        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }
        public ICollection<CheckList> CheckLists { get; set; } = new List<CheckList>();
    }
}
