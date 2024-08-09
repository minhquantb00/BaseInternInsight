using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.UserRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseUser>> RegisterUser(Request_Register registerUser);
        Task AssignRoleToUserAsync(List<string> roles, ApplicationUser user);
        Task<ResponseObject<DataResponseLoginOTP>> GetOtpByLoginAsync(Request_Login loginModel);
        Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(ApplicationUser user);
        Task<ResponseObject<DataResponseLogin>> LoginUserWithJWTokenAsync(string otp, string userName);
        Task<ResponseObject<DataResponseLogin>> RenewAccessTokenAsync(Request_RenewAccessToken token);
    }
}
