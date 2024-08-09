using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    public class ConfirmEmail : BaseEntity
    {
        public string ConfirmCode { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsConfirm {  get; set; }
    }
}
