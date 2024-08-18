using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.ImplementServices
{
    public class ContractTypeService : IContractTypeService
    {
        private readonly IRepository<ContractType> _contractTypeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ContractTypeConverter _contractTypeConverter;
        public ContractTypeService(IRepository<ContractType> contractTypeRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, ContractTypeConverter contractTypeConverter)
        {
            _contractTypeRepository = contractTypeRepository;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _contractTypeConverter = contractTypeConverter;
        }

        public async Task<ResponseObject<DataResponseContractType>> CreateContractType(Request_CreateContractType request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseContractType>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Tài khoản chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseContractType>
                    {
                        Data = null,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Status = StatusCodes.Status403Forbidden
                    };
                }
                ContractType contractType = new ContractType
                {
                    Description = request.Description,
                    Name = request.Name,
                    Id = Guid.NewGuid()
                };
                contractType = await _contractTypeRepository.CreateAsync(contractType);
                return new ResponseObject<DataResponseContractType>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo loại hợp đồng thành công",
                    Data = _contractTypeConverter.EntityToDTO(contractType)
                };
            }catch (Exception ex)
            {
                return new ResponseObject<DataResponseContractType>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<string> DeleteContractType(Guid contractTypeId)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Tài khoản chưa xác thực";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }
                
                var contractType = await _contractTypeRepository.GetAsync(record => record.Id == contractTypeId);
                if (contractType == null) return "Loại hợp đồng không tồn tại";
                _contractTypeRepository.Delete(contractType);

                return "Xóa loại hợp đồng thành công";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IQueryable<DataResponseContractType>> GetAllContractTypes(Request_FilterContractType? request)
        {
            var query = await _contractTypeRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.AsNoTracking().Where(record => record.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var result = query.AsNoTracking().Select(item => _contractTypeConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseContractType> GetContractTypeById(Guid contractTypeId)
        {
            var contractType = await _contractTypeRepository.GetAsync(record => record.Id == contractTypeId);
            if (contractType == null) return null;
            var result = _contractTypeConverter.EntityToDTO(contractType);
            return result;
        }

        public async Task<ResponseObject<DataResponseContractType>> UpdateContractType(Request_UpdateContractType request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseContractType>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Tài khoản chưa xác thực",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseContractType>
                    {
                        Data = null,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Status = StatusCodes.Status403Forbidden
                    };
                }
                var contractType = await _contractTypeRepository.GetAsync(record => record.Id == request.Id);
                if(contractType == null)
                {
                    return new ResponseObject<DataResponseContractType>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Loại hợp đồng không tồn tại",
                        Data= null
                    };
                }
                contractType.Name = !string.IsNullOrEmpty(request.Name) ? request.Name : contractType.Name;
                contractType.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : contractType.Description;
                contractType = await _contractTypeRepository.UpdateAsync(contractType);
                return new ResponseObject<DataResponseContractType>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật loại hợp đồng thành công",
                    Data = _contractTypeConverter.EntityToDTO(contractType)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseContractType>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}
