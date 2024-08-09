using BaseInsightDotNet.Business.ImplementServices;
using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
using BaseInsightDotNet.Core.Entities;
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
        }
    }
}
