using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IUserService
    {
        Task<string> DeleteUser(string userId);
        Task<IQueryable<DataResponseUser>> GetAllUsers(Request_FilterUser? request);
        Task<DataResponseUser> GetUserById(string userId);
    }
}
