using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Base
{
    public class BaseEntitySoftDeletetable : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
