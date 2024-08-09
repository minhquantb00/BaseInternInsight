using BaseInsightDotNet.Business.ImplementServices;
using BaseInsightDotNet.Business.InterfaceServices;
using BaseInsightDotNet.Business.Payloads.Converters;
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
        }
    }
}
