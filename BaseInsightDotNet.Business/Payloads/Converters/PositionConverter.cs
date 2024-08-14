using BaseInsightDotNet.Business.Payloads.ResponseModels.DataPosition;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.Converters
{
    public class PositionConverter
    {
        public DataResponsePosition EntityToDTO(Position position)
        {
            return new DataResponsePosition
            {
                Id = position.Id,
                Name = position.Name,
                SalaryCoefficient = position.SalaryCoefficient,
            };
        }
    }
}
