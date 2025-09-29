using Gestran.Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gestran.Backend.Domain.Entities
{
    public class CheckList
    {
        public Guid Id { get; set; }
        public Guid? ExecutedById { get; set; }
        public User? ExecutedBy { get; set; }

        public Guid? CollectionId { get; set; }
        public CheckListCollection? Collection { get; set; }



        public string CheckListName { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CheckListStatus CurrentStatus { get; set; }
        public bool InProgress { get; set; }
        public bool IsApproved { get; set; }
        public string? GeneralComments { get; set; }
        public List<CheckListItem> CheckListItems { get; set; } = new List<CheckListItem>();
    }
}
