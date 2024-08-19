using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.AllowanceRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataAllowance;
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
    public class AllowanceService : IAllowanceService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AllowanceConverter _allowanceConverter;
        private readonly IRepository<Allowance> _allowanceRepository;
        public AllowanceService(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, AllowanceConverter allowanceConverter, IRepository<Allowance> allowanceRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _allowanceConverter = allowanceConverter;
            _allowanceRepository = allowanceRepository;
        }

        public async Task<ResponseObject<DataResponseAllowance>> CreateAllowance(Request_CreateAllowance request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseAllowance>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Tài khoản chưa được xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseAllowance>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }

                Allowance allowance = new Allowance
                {
                    AllowanceName = request.AllowanceName,
                    Amount = request.Amount,
                    Id = Guid.NewGuid(),
                };
                allowance = await _allowanceRepository.CreateAsync(allowance);

                return new ResponseObject<DataResponseAllowance>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thông tin phúc lợi thành công",
                    Data = _allowanceConverter.EntityToDTO(allowance)
                };
            }catch(Exception ex)
            {
                return new ResponseObject<DataResponseAllowance>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<string> DeleteAllowance(Guid id)
        {
            var currentUser = _contextAccessor.HttpContext.User;
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

                var allowance = await _allowanceRepository.GetAsync(record => record.Id == id);
                if (allowance == null) return "Thông tin phúc lợi không tồn tại";

                _allowanceRepository.Delete(allowance);

                return "Xóa thông tin phúc lợi thành công";
            }catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public async Task<IQueryable<DataResponseAllowance>> GetAllAllowances(Request_FilterAllowance? request)
        {
            var query = await _allowanceRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.AsNoTracking().Where(record => record.AllowanceName.ToLower().Contains(request.Name.ToLower()));
            }

            var result = query.AsNoTracking().Select(item => _allowanceConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseAllowance> GetAllowanceById(Guid id)
        {
            var allowance = await _allowanceRepository.GetAsync(record => record.Id == id);
            if (allowance == null) return null;
            return _allowanceConverter.EntityToDTO(allowance);
        }

        public async Task<ResponseObject<DataResponseAllowance>> UpdateAllowance(Request_UpdateAllowance request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseAllowance>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Tài khoản chưa được xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseAllowance>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null
                    };
                }

                var allowance = await _allowanceRepository.GetAsync(record => record.Id == request.Id);
                if(allowance == null)
                {
                    return new ResponseObject<DataResponseAllowance>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Thông tin phúc lợi không tồn tại",
                        Data = null
                    };
                }
                allowance.AllowanceName = request.AllowanceName;
                allowance.Amount = request.Amount;
                allowance = await _allowanceRepository.UpdateAsync(allowance);

                return new ResponseObject<DataResponseAllowance>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật thông tin phúc lợi thành công",
                    Data = _allowanceConverter.EntityToDTO(allowance)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseAllowance>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
