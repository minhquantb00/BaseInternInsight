using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.DepartmentRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataDepartment;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DepartmentConverter _departmentConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ApplicationUser> _userRepository;
        public DepartmentService(IRepository<Department> departmentRepository, DepartmentConverter departmentConverter, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IRepository<ApplicationUser> userRepository)
        {
            _departmentRepository = departmentRepository;
            _departmentConverter = departmentConverter;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<DataResponseDepartment>> CreateDepartment(Request_CreateDepartment request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Data = null,
                        Message = "Tài khoản chưa được xác thực",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Data = null,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Status = StatusCodes.Status400BadRequest
                    };
                }

                var manager = await _userManager.FindByIdAsync(request.ManagerId);
                if (manager == null)
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin người dùng",
                        Data = null
                    };
                }

                Department department = new Department
                {
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ManagerId = request.ManagerId,
                    Name = request.Name,
                    NumberOfMember = 0,
                    Slogan = request.Slogan
                };
                department = await _departmentRepository.CreateAsync(department);

                return new ResponseObject<DataResponseDepartment>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thêm phòng ban thành công",
                    Data = _departmentConverter.EntityToDTO(department)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDepartment>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public async Task<string> DeleteDepartment(Guid departmentId)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;

            if (!currentUser.Identity.IsAuthenticated)
            {
                return "Tài khoản chưa được xác thực";
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return "Bạn không có quyền thực hiện chức năng này";
            }

            var department = await _departmentRepository.GetAsync(record => record.Id == departmentId);
            if (department == null) return "Phòng ban không tồn tại";

             _departmentRepository.Delete(department);

            return "Xóa phòng ban thành công";
        }

        public async Task<IQueryable<DataResponseDepartment>> GetAllDepartments(Request_FilterDepartment? request)
        {
            var query = await _departmentRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.AsNoTracking().Where(record => record.Name.ToLower().Contains(request.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.ManagerId))
            {
                query = query.AsNoTracking().Where(record => record.ManagerId.Equals(request.ManagerId));
            }

            var result = query.AsNoTracking().Select(item => _departmentConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseDepartment> GetDepartmentById(Guid departmentId)
        {
            var department = await _departmentRepository.GetAsync(record => record.Id == departmentId);
            if (department == null) return null;

            var result = _departmentConverter.EntityToDTO(department);
            return result;
        }

        public async Task<ResponseObject<DataResponseDepartment>> UpdateDepartment(Request_UpdateDepartment request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Data = null,
                        Message = "Tài khoản chưa được xác thực",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Data = null,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Status = StatusCodes.Status400BadRequest
                    };
                }

                var department = await _departmentRepository.GetAsync(record => record.Id == request.Id);
                if(department == null)
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy phòng ban",
                        Data = null
                    };
                }
                var manager = await _userRepository.GetAsync(record => record.Id.Equals(request.ManagerId));
                if(manager == null)
                {
                    return new ResponseObject<DataResponseDepartment>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin trưởng phòng không hợp lệ",
                        Data = null
                    };
                }
                department.Slogan = !string.IsNullOrEmpty(request.Slogan) ? request.Slogan : department.Slogan;
                department.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : department.Name;
                department.ManagerId = !string.IsNullOrEmpty(request.ManagerId) && await _userManager.FindByIdAsync(request.ManagerId) != null ? request.ManagerId : department.ManagerId;
                department.UpdateTime = DateTime.Now;
                department = await _departmentRepository.UpdateAsync(department);

                return new ResponseObject<DataResponseDepartment>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật phòng ban thành công",
                    Data = _departmentConverter.EntityToDTO(department)
                };

            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseDepartment>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
