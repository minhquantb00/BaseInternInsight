using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.DataAccess.Tracing
{
    public class DefaultCorrelationIdProvider : ICorrelationIdProvider
    {
        public string Get()
        {
            return CreateNewCorrelationId();
        }

        protected virtual string CreateNewCorrelationId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
