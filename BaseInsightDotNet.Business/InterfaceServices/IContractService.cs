using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Commons.Enums;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IContractService
    {
        Task<ResponseObject<DataResponseContract>> CreateContract(Request_CreateContract request);
        Task<ResponseObject<DataResponseContract>> UpdateContract(Request_UpdateContract request);
        Task<string> DeleteContract(Guid id);
        Task<DataResponseContract> GetContractById(Guid id);
        Task<IQueryable<DataResponseContract>> GetAllContracts(Request_FilterContract? request);
        Task UploadPhotoContract(Request_UploadPhotoContract request);

        Task ContractInteraction(Guid contractId, Enumerate.ContractStatus status);
    }
}
