using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserConverter _userConverter;

        public UserService(IRepository<Department> departmentRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IRepository<ApplicationUser> userRepository, UserConverter userConverter)
        {
            _departmentRepository = departmentRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userConverter = userConverter;
        }
        public async Task<string> DeleteUser(string userId)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Tài khoản chưa được xác thực";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return "Người dùng không tồn tại";

                user.IsDeleted = true;
                await _userRepository.UpdateAsync(user);
                return "Xóa thông tin người dùng thành công";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IQueryable<DataResponseUser>> GetAllUsers(Request_FilterUser? request)
        {
            var query = await _userRepository.GetAllAsync(item => item.IsDeleted == false);
            if (!string.IsNullOrEmpty(request.KeyWord))
            {
                query = query.AsNoTracking().Where(record => record.UserName.Equals(request.KeyWord) || record.Email.Equals(request.KeyWord) || record.PhoneNumber.Equals(request.KeyWord) || record.FullName.ToLower().Contains(request.KeyWord.ToLower()));
            }
            if (request.DepartmentId.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.DepartmentId ==  request.DepartmentId);
            }
            if(request.PositionId.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.PositionId == request.PositionId);
            }

            var result = query.AsNoTracking().Select(item => _userConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseUser> GetUserById(string userId)
        {
            var query = await _userManager.FindByIdAsync(userId);
            if (query == null || query.IsDeleted == true) return null;

            return _userConverter.EntityToDTO(query);
        }
    }
}
