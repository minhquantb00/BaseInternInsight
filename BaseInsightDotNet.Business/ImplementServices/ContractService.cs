using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.ContractRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Commons.Enums;
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
                        Data = null
                    };
                }
                var contractType = await _contractTypeRepository.GetAsync(record => record.Id == request.ContractTypeId);
                if (contractType == null)
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin loại hợp đồng",
                        Data = null
                    };
                }

                var employee = await _userManager.FindByIdAsync(request.EmployeeId);
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
                    contract.IsSubsidized = true;
                    contract.ReceiveAllowance = true;
                }


                contract = await _contractRepository.CreateAsync(contract);

                return new ResponseObject<DataResponseContract>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Tạo thông tin hợp đồng thành công",
                    Data = _contractConverter.EntityToDTO(contract),
                };

            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IQueryable<DataResponseContract>> GetAllContracts(Request_FilterContract? request)
        {
            var query = await _contractRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                query = query.AsNoTracking().Where(record => record.EmployeeId.Equals(request.EmployeeId));
            }
            if (request.ContractTypeId.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.ContractTypeId == request.ContractTypeId);
            }
            if (request.ContractStatus.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.ContractStatus == request.ContractStatus);
            }
            if (request.FromDate.HasValue && request.ToDate.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.StartDate >= request.FromDate && record.StartDate <= request.ToDate);
            }
            if (request.FromDate.HasValue && !request.ToDate.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.StartDate >= request.FromDate);
            }
            if (!request.FromDate.HasValue && request.ToDate.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.StartDate <= request.ToDate);
            }

            var result = query.AsNoTracking().Select(item => _contractConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseContract> GetContractById(Guid id)
        {
            var query = await _contractRepository.GetByIdAsync(id);
            if (query == null) return null;
            return _contractConverter.EntityToDTO(query);
        }


        public async Task<ResponseObject<DataResponseContract>> UpdateContract(Request_UpdateContract request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;

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
                    Data = null
                };
            }
            var contract = await _contractRepository.GetByIdAsync(request.Id);
            if (contract == null)
            {
                return new ResponseObject<DataResponseContract>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Không tìm thấy thông tin hợp đồng",
                    Data = null
                };
            }
            ContractType contractType = new ContractType();
            if (request.ContractTypeId != null && request.ContractTypeId != Guid.Empty)
            {
                contractType = await _contractTypeRepository.GetAsync(record => record.Id == request.ContractTypeId);
                if (contractType == null)
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin loại hợp đồng",
                        Data = null
                    };
                }
            }
            ApplicationUser employee = new ApplicationUser();
            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                employee = await _userManager.FindByIdAsync(request.EmployeeId);
                if (employee == null)
                {
                    return new ResponseObject<DataResponseContract>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Không tìm thấy thông tin nhân viên",
                        Data = null
                    };
                }
            }

            contract.StartDate = request.StartDate != null ? request.StartDate : contract.StartDate;
            contract.EndDate = request.EndDate != null ? request.EndDate : contract.EndDate;
            contract.ContractTypeId = request.ContractTypeId != null && request.ContractTypeId != Guid.Empty ? request.ContractTypeId : contract.ContractTypeId;
            contract.Content = !string.IsNullOrEmpty(request.Content) ? request.Content : contract.Content;
            contract.BaseSalary = request.BaseSalary != null ? request.BaseSalary : contract.BaseSalary;
            contract.EmployeeId = !string.IsNullOrEmpty(request.EmployeeId) ? request.EmployeeId : contract.EmployeeId;
            contract.TaxPercentage = request.TaxPercentage != null ? request.TaxPercentage : contract.TaxPercentage;
            contract.SalaryBeforeTax = request.TaxPercentage != null && request.BaseSalary != null ? request.BaseSalary - (request.BaseSalary * request.TaxPercentage) : contract.SalaryBeforeTax;

            if (contractType != null)
            {
                if (contractType.Name.Equals("CTV") || contractType.Name.Equals("Thử việc"))
                {
                    contract.IsSubsidized = false;
                    contract.ReceiveAllowance = false;
                }
                else
                {
                    contract.IsSubsidized = true;
                    contract.ReceiveAllowance = true;
                }
            }


            try
            {
                contract = await _contractRepository.UpdateAsync(contract);

                return new ResponseObject<DataResponseContract>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật thông tin hợp đồng thành công",
                    Data = _contractConverter.EntityToDTO(contract),
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseContract>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }


        }

        public async Task UploadPhotoContract(Request_UploadPhotoContract request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            string userId = currentUser.FindFirst("Id").Value;

            try
            {
                var contract = await _contractRepository.GetByIdAsync(request.ContractId);
                if (contract == null)
                    throw new ArgumentNullException(nameof(contract));

                var defaultFolder = _mediaService.GetMediaFolders("FilesUpload").FirstOrDefault();
                if (defaultFolder == null)
                {
                    defaultFolder = await _mediaService.CreateFolder(new Core.Entities.Media.MediaFolder { IsPublic = true, Name = "Photos" }, userId);
                }

                if (request.Files.Count > 2)
                    throw new ArgumentException("Chỉ cho phép upload tối đa 2 ảnh cho chữ ký.");

                var signatureFiles = new Dictionary<string, string>();
                int counter = 1;

                foreach (var file in request.Files)
                {
                    var mediaFile = await _mediaService.SaveFileAsync(
                        defaultFolder.Id,
                        file.FileName,
                        file.SavePath,
                        file.FileStream,
                        true,
                        userId,
                        false);

                    if (counter == 1)
                        signatureFiles["SignatureA"] = mediaFile.FileKey.ToString();
                    else if (counter == 2)
                        signatureFiles["SignatureB"] = mediaFile.FileKey.ToString();

                    counter++;
                }
                foreach (var entry in signatureFiles)
                {
                    var propertyInfo = contract.GetType().GetProperty(entry.Key);
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(contract, entry.Value);
                    }
                }

                await _contractRepository.UpdateAsync(contract);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("Hợp đồng không tồn tại.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Quyền truy cập bị từ chối.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình upload và lưu ảnh.", ex);
            }
        }

    }
}
