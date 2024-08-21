using BaseInsightDotNet.Business.Payloads.ResponseModels.DataAllowance;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.Converters
{
    public class AllowanceConverter
    {
        public DataResponseAllowance EntityToDTO(Allowance allowance)
        {
            return new DataResponseAllowance
            {
                Id = allowance.Id,
                AllowanceName = allowance.AllowanceName,
                Ammount = allowance.Amount
            };
        }
    }
}
