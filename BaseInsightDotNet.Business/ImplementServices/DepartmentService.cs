using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataDepartment;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
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
        private readonly DepartmentConverter _departmentConverter;
        public DepartmentService(IRepository<Department> departmentRepository, DepartmentConverter departmentConverter)
        {
            _departmentRepository = departmentRepository;
            _departmentConverter = departmentConverter;
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
    }
}
