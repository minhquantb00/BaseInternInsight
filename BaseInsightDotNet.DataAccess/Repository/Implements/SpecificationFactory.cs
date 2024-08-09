using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.DataAccess.Repository.Implements
{
    public class SpecificationFactory : ISpecificationFactory
    {
        public ISpecification<T> Create<T>()
        {
            return new NullSpecification<T>();
        }
    }
}
