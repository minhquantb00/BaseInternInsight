using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities.Media
{
    public partial class MediaStorage : BaseEntity
    {
        public byte[] Data { get; set; }
    }
}
