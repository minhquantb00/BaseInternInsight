using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.DataAccess.Repository.Interfaces
{
    public interface ISpecificationFactory
    {
        ISpecification<T> Create<T>();
    }
}
