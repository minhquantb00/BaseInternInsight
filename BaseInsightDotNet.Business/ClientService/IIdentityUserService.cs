using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ClientService
{
    public interface IIdentityUserService
    {
        Task AddUserRolesAsync(string userId, List<string> roles);
        Task RemoveUserRolesAsync(string userId, List<string> roles);
        Task AddUserClaimsAsync(string userId, Dictionary<string, string> claims);
        Task RemoveUserClaimsAsync(string userId, Dictionary<string, string> claims);
        Task<List<string>> GetUserClaimsByTypeAsync(string userName, string type);
        Task AddUserClaimsByTypeAsync(string userName, string type, List<string> claims);
        Task RemoveUserClaimsByTypeAsync(string userName, string type);
        //Task<UseCasePagedOutput<UserViewModel>> GetUsers(string queryString = null);
        //Task<GetUserUseCaseOutput> GetUserById(string userId);
        //Task<GetUserUseCaseOutput> GetUserByUsername(string userId);
        //Task<CreateUserUseCaseOutput> CreateUser(string inputString);
        //Task<CreateUserUseCaseOutput> CreateUser(CreateUserUseCaseInput input);
        //Task<UpdateUserUseCaseOutput> UpdateUser(string userId, UpdateUserUseCaseInput input);
        //Task<UpdateUserUseCaseOutput> UpdateUser(string userId, string inputString);
        //Task<RemoveUserUseCaseOutput> RemoveUser(string userId);
        //Task<UseCasePagedOutput<RoleViewModel>> GetRoles(string queryString = null);
        //Task<ResetPasswordUseCaseOutput> ResetPasswordById(string userId, string inputString);
    }
}
