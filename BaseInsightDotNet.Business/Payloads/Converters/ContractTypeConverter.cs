using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.Converters
{
    public class ContractTypeConverter
    {
        public DataResponseContractType EntityToDTO(ContractType contractType)
        {
            return new DataResponseContractType
            {
                Id = contractType.Id,
                Description = contractType.Description,
                Name = contractType.Name,
            };
        }
    }
}
