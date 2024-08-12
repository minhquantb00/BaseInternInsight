using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("Department_tbl")]
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Slogan { get; set; }
        public int NumberOfMember { get; set; } = 0;
        public string ManagerId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual ICollection<ApplicationUser>? Users { get; set; }
    }
}
