using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
