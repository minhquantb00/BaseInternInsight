using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.DataAccess.Tracing
{
    public interface ICorrelationIdProvider
    {
        string Get();
    }
}
