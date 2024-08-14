using BaseInsightDotNet.Business.Payloads.ResponseModels.DataPosition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IPositionService
    {
        Task<IQueryable<DataResponsePosition>> GetAllPositions();
    }
}
