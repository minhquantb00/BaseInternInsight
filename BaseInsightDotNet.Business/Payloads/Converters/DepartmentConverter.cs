using BaseInsightDotNet.Business.Payloads.ResponseModels.DataDepartment;
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
    public class DepartmentConverter
    {
        private readonly IRepository<ApplicationUser> _repository;
        private readonly UserConverter _userConverter;
        public DepartmentConverter(IRepository<ApplicationUser> repository, UserConverter userConverter)
        {
            _repository = repository;
            _userConverter = userConverter;
        }
        public DataResponseDepartment EntityToDTO(Department entity)
        {
            var manager = _repository.GetAsync(item => item.Id.Equals(entity.ManagerId));
            return new DataResponseDepartment
            {
                Id = entity.Id,
                CreateTime = entity.CreateTime,
                Manager = manager.Result != null ? _userConverter.EntityToDTO(manager.Result) : null,
                Name = entity.Name,
                NumberOfMember = entity.NumberOfMember,
                Slogan = entity.Slogan,
            };
        }
    }
}
