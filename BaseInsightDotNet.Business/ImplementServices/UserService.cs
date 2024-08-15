using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ImplementServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ApplicationUser> _userRepository;

        public UserService(IRepository<Department> departmentRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IRepository<ApplicationUser> userRepository)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public Task<string> DeleteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<DataResponseUser>> GetAllUsers(Request_FilterUser? request)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseUser> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
