using BaseInsightDotNet.Business.Payloads.RequestModels.MediaRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest
{
    public class Request_UploadPhotoContract
    {
        public Guid ContractId { get; set; }
        public List<Request_UploadPhoto> Files { get; set; }
    }
}
