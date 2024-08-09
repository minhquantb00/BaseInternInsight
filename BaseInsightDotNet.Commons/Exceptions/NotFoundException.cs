using BaseInsightDotNet.Commons.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Commons.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Name { get; private set; }

        public NotFoundException(string name)
        {
            Name = name;
        }
        public override string Message => string.Format(Constant.ExceptionMessage.NOT_FOUND, Name);
    }
}
