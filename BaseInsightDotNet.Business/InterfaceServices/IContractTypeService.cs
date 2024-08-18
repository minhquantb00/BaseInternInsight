using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IContractTypeService
    {
        Task<ResponseObject<DataResponseContractType>> CreateContractType(Request_CreateContractType request);
        Task<ResponseObject<DataResponseContractType>> UpdateContractType(Request_UpdateContractType request);
        Task<string> DeleteContractType(Guid contractTypeId);
        Task<IQueryable<DataResponseContractType>> GetAllContractTypes(Request_FilterContractType? request);
        Task<DataResponseContractType> GetContractTypeById(Guid contractTypeId);
    }
}
