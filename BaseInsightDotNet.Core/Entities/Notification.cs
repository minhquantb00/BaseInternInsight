using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("Notification_tbl")]
    public class Notification : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }  
        public string? Content { get; set; }
        public string? Image {  get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsSeen { get; set; } = false;
    }
}
