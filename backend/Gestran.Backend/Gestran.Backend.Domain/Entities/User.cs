using Gestran.Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccessHashCode { get; set; } = string.Empty;
        public bool IsAccessActive { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }

        public ICollection<CheckListCollection> OwnedCollections { get; set; } = new List<CheckListCollection>();
        public ICollection<CheckList> ExecutedCheckLists { get; set; } = new List<CheckList>();
    }
}
