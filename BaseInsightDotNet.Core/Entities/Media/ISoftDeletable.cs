using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities.Media
{
    public interface ISoftDeletable
    {
        bool Deleted { get; set; }
    }
}
