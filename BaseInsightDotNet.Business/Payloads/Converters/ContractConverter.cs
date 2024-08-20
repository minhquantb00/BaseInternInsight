using BaseInsightDotNet.Business.Payloads.ResponseModels.DataContract;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataUser;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.Payloads.Converters
{
    public class ContractConverter
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<ContractType> _contractTypeRepository;
        private readonly UserConverter _userConverter;
        public ContractConverter(IRepository<ApplicationUser> userRepository, IRepository<ContractType> contractTypeRepository, UserConverter userConverter)
        {
            _userRepository = userRepository;
            _contractTypeRepository = contractTypeRepository;
            _userConverter = userConverter;
        }

        public DataResponseContract EntityToDTO(Contract contract)
        {
            var user = _userRepository.GetAsync(record => record.Id == contract.EmployeeId).Result;
            var contractType = _contractTypeRepository.GetAsync(record => record.Id == contract.ContractTypeId).Result;
            return new DataResponseContract
            {
                ReceiveAllowance = contract.ReceiveAllowance == true ? "Được nhận trợ cấp" : "Không được nhận trợ cấp",
                SignatureA = contract.SignatureA,
                BaseSalary = contract.BaseSalary,
                Code = contract.Code,
                Content = contract.Content,
                ContractStatus = contract.ContractStatus.ToString(),
                ContractTypeName = contractType != null ? contractType.Name : "",
                Employee = user != null ? _userConverter.EntityToDTO(user) : null,
                EndDate = contract.EndDate,
                Id = contract.Id,
                IsSubsidized = contract.IsSubsidized == true ? "Được trợ cấp" : "Không được trợ cấp",
                SalaryBeforeTax = contract.SalaryBeforeTax,
                SignatureB = contract.SignatureB,
                StartDate = contract.StartDate,
                TaxPercentage = contract.TaxPercentage,

            };
        }
    }
}
