using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Commons.Enums;
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
    public class ContractService : IContractService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Contract> _contractRepository;
        private readonly ContractConverter _contractConverter;
        private readonly IRepository<ContractType> _contractTypeRepository;
        private readonly IMediaService _mediaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContractService(IHttpContextAccessor httpContextAccessor, IRepository<Contract> contractRepository, ContractConverter contractConverter, IRepository<ContractType> contractTypeRepository, IMediaService mediaService, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _contractConverter = contractConverter;
            _contractTypeRepository = contractTypeRepository;
            _mediaService = mediaService;
            _contractRepository = contractRepository;
            _userManager = userManager;
        }

        public Task ContractInteraction(Guid contractId, Enumerate.ContractStatus status)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<DataResponseContract>> CreateContract(Request_CreateContract request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Tài khoản chưa được kích hoạt",
                        Data = null
                    };
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không đủ quyền thực hiện chức năng này",
                        Data= null
                    };
                }
                var contractType = await _contractTypeRepository.GetAsync(record => record.Id == request.ContractTypeId);
                if(contractType == null)
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin loại hợp đồng",
                        Data = null
                    };
                }

                var  employee = await _userManager.FindByIdAsync(request.EmployeeId);
                if (employee == null)
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin nhân viên",
                        Data = null 
                    };
                }

                Contract contract = new Contract
                {
                    SignatureA = "",
                    BaseSalary = request.BaseSalary,
                    Code = "Quan_" + DateTime.Now.Ticks.ToString(),
                    Content = request.Content,
                    ContractStatus = Commons.Enums.Enumerate.ContractStatus.DangCho,
                    ContractTypeId = request.ContractTypeId,
                    EmployeeId = request.EmployeeId,
                    EndDate = request.EndDate,
                    Id = Guid.NewGuid(),
                    SignatureB = "",
                    StartDate = request.StartDate,
                    TaxPercentage = request.TaxPercentage,
                    SalaryBeforeTax = request.BaseSalary - (request.BaseSalary * request.TaxPercentage)
                };

                if (contractType.Name.Equals("CTV") || contractType.Name.Equals("Thử việc"))
                {
                    contract.IsSubsidized = false;
                    contract.ReceiveAllowance = false;
                }
                else
                {
                    contract.IsSubsidized = false;
                    contract.ReceiveAllowance = false;
                }


                contract = await _contractRepository.CreateAsync(contract);

                return new ResponseObject<DataResponseContract>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thông tin hợp đồng thành công",
                    Data = _contractConverter.EntityToDTO(contract),
                };

            }catch (Exception ex)
            {
                return new ResponseObject<DataResponseContract>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<string> DeleteContract(Guid id)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Tài khoản chưa được kích hoạt";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }

                var contract = await _contractRepository.GetAsync(record => record.Id == id);
                if (contract == null)
                {
                    return "Hợp đồng không tồn tại";
                }

                await _contractRepository.DeleteAsync(contract.Id);

                return "Xóa hợp đồng thành công";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
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

        public Task UploadPhotoContract(Guid contractId)
        {
            throw new NotImplementedException();
        }
    }
}
