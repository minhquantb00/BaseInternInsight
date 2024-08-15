using BaseInsightDotNet.Business.Payloads;
using BaseInsightDotNet.Business.Payloads.RequestModels.DepartmentRequest;
using BaseInsightDotNet.Business.Payloads.RequestModels.FilterRequest;
using BaseInsightDotNet.Business.Payloads.ResponseModels.DataDepartment;
using BaseInsightDotNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IDepartmentService
    {
        Task<IQueryable<DataResponseDepartment>> GetAllDepartments(Request_FilterDepartment? request);
        Task<ResponseObject<DataResponseDepartment>> CreateDepartment(Request_CreateDepartment request);
        Task<ResponseObject<DataResponseDepartment>> UpdateDepartment(Request_UpdateDepartment request);
        Task<string> DeleteDepartment(Guid departmentId);
        Task<DataResponseDepartment> GetDepartmentById(Guid departmentId);
    }
}
