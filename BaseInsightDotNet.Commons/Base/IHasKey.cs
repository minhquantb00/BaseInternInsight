using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Base
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}
