using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ImplementServices
{
    public class ContractService : IContractService
    {
        public Task<ResponseObject<DataResponseContract>> CreateContract(Request_CreateContract request)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteContract(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<DataResponseContract>> GetAllContracts()
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseContract> GetContractById(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<ResponseObject<DataResponseContract>> UpdateContract(Request_UpdateContract request)
        {
            throw new NotImplementedException();
        }
    }
}
