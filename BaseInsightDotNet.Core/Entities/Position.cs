using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("Position_tbl")]
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public decimal SalaryCoefficient { get; set; }
        public virtual ICollection<ApplicationUser>? Users { get; set; }
    }
}
