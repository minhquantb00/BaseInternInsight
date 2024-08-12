using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("ContractAllowance_tbl")]
    public class ContractAllowance : BaseEntity
    {
        public Guid ContractId { get; set; }
        public virtual Contract? Contract { get; set; }
        public Guid AllowanceId { get; set; }
        public virtual Allowance? Allowance { get; set; }
    }
}
