using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("ContractHistory_tbl")]
    public class ContractHistory : BaseEntity
    {
        public Guid ContractId { get; set; }
        public virtual Contract? Contract { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangedBy { get; set; }
        public string ChangeDetails { get; set; }
    }
}
