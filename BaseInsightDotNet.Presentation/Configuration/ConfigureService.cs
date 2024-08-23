using BaseInsightDotNet.Business.Handle.Media;
using BaseInsightDotNet.Business.ImplementServices;
using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Commons.Media;
using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.Core.Entities.Media;
using BaseInsightDotNet.DataAccess.Data;
using BaseInsightDotNet.DataAccess.Repository.Implements;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using BaseInsightDotNet.DataAccess.UnitOfWork.Implements;
using BaseInsightDotNet.DataAccess.UnitOfWork.Interfaces;

namespace BaseInsightDotNet.Presentation.Configuration
{
    public static class ConfigureService
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<UserConverter>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
            services.AddHttpContextAccessor();
            services.AddScoped<IDbContext, IdentityDbContext>();
            services.AddScoped<IRepository<ApplicationUserRole>, Repository<ApplicationUserRole>>();
            services.AddScoped<IRepository<ApplicationRole>,  Repository<ApplicationRole>>();
            services.AddScoped<IRepository<MediaFile>, Repository<MediaFile>>();
            services.AddScoped<IRepository<MediaFolder>, Repository<MediaFolder>>();
            services.AddScoped<IMediaService,  MediaService>();
            services.AddScoped<IMediaTypeResolver, MediaTypeResolver>();
            services.AddScoped<ISpecificationFactory, SpecificationFactory>();
            services.AddScoped<IImageProcessor, DefaultImageProcessor>();
            services.AddScoped<DepartmentConverter>();
            services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();
            services.AddScoped<IRepository<Department>, Repository<Department>>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IRepository<Position>, Repository<Position>>();
            services.AddScoped<PositionConverter>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepository<ContractType>, Repository<ContractType>>();
            services.AddScoped<ContractTypeConverter>();
            services.AddScoped<IContractTypeService, ContractTypeService>();
            services.AddScoped<IRepository<Allowance>, Repository<Allowance>>();
            services.AddScoped<AllowanceConverter>();
            services.AddScoped<IAllowanceService, AllowanceService>();
            services.AddScoped<ContractConverter>();
            services.AddScoped<IRepository<Contract>, Repository<Contract>>();
        }
    }
}
