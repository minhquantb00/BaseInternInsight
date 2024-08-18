using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("Payroll_tbl")]
    public class Payroll : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal? OvertimeSalary { get; set; }
        public decimal? AllowanceSalary { get; set; }
        public decimal? ProductivitySalary { get; set; }
        public decimal? OtherIncome {  get; set; }
        public decimal ToltalSalary { get; set; }
        public int Month {  get; set; }
        public int Year { get; set; }
    }
}
