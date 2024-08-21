using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.AllowanceRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataAllowance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IAllowanceService
    {
        Task<ResponseObject<DataResponseAllowance>> CreateAllowance(Request_CreateAllowance request);
        Task<ResponseObject<DataResponseAllowance>> UpdateAllowance(Request_UpdateAllowance request);
        Task<string> DeleteAllowance(Guid id);
        Task<IQueryable<DataResponseAllowance>> GetAllAllowances(Request_FilterAllowance? request);
        Task<DataResponseAllowance> GetAllowanceById(Guid id);
    }
}
