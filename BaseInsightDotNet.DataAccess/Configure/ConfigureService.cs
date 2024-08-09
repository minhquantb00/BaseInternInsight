using BaseInsightDotNet.Core.Entities;
using BaseInsightDotNet.DataAccess.Data;
using BaseInsightDotNet.DataAccess.Delegation;
using BaseInsightDotNet.DataAccess.SeedData;
using BaseInsightDotNet.DataAccess.Tracing;
using Duende.IdentityServer;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static BaseInsightDotNet.Commons.Constants.Constant;

namespace BaseInsightDotNet.DataAccess.Configure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBaseInfrastructure();
            services.AddScoped<IExtensionGrantValidator, DelegationGrantValidator>();
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                sqlServerOptionsAction: options => {
                    options.EnableRetryOnFailure();
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                }));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                                .AddDefaultTokenProviders();
            var builder = services.AddIdentityServer(options => {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(configuration.GetSection("IdentityServer:IdentityResources"))
                .AddInMemoryApiScopes(configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"))
                .AddInMemoryApiResources(configuration.GetSection("IdentityServer:ApiResources"))
                .AddAspNetIdentity<ApplicationUser>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(99.0);

                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/api/auth/logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = false;
            });
            services.AddAuthentication()
                        .AddMicrosoftAccount(microsoftOptions =>
                        {
                            microsoftOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                            microsoftOptions.ClientId = "91eac13f-f506-42ad-92e9-c270b16c6e83";
                            microsoftOptions.ClientSecret = ".X55Ljxf/ut?[FwsMouFYT9KGIG5Y3iU";
                        })
                        .AddFacebook(facebookOptions =>
                        {
                            facebookOptions.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                            facebookOptions.AppId = "614218869377179";
                            facebookOptions.AppSecret = "7b81c3365e3a20ef205a05980b5204dd";
                        })
                        .AddGoogle(options =>
                        {
                            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                            options.ClientId = "585809915555-74bvojk3mbf3gsqasp3hgmauhhc5e2pc.apps.googleusercontent.com";
                            options.ClientSecret = "L2417ys8xO7nbE3d4zxg7yvX";
                        });

            services.AddLocalApiAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });

                options.AddPolicy(Roles.Admin, policy =>
                {
                    policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddCors(x => x.AddPolicy("corsGlobalPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:5173", "http://localhost:5174")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            }));
            return services;
        }
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<CorrelationIdOptions>();
            services.AddSingleton<ICorrelationIdProvider, AspNetCoreCorrelationIdProvider>();
            services.AddTransient<CorrelationIdMiddleware>();
            return services;
        }
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
