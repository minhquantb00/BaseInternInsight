using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("Allowance_tbl")]
    public class Allowance : BaseEntity
    {
        public string AllowanceName { get; set; }
        public double Amount { get; set; }
        public virtual ICollection<ContractAllowance>? ContractAllowances { get; set; }
    }
}
