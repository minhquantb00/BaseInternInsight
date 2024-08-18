using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("ContractAppendix_tbl")]
    public class ContractAppendix : BaseEntity
    {
        public Guid ContractId { get; set; }
        public virtual Contract? Contract { get; set; }
        public DateTime AppendixDate { get; set; }
        public string Details { get; set; }
    }
}
