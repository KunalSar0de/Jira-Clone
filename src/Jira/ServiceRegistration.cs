using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.EFCore;
using Jira.Repository;
using Jira.Repository.Impl;
using Jira.Services;
using Jira.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jira
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(IServiceCollection  service)
        {

            /*Services------------------------------------------------------*/

            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            service.AddSingleton<ITokenHelperService, TokenHelperService>();
            service.AddScoped<ISessionService, SessionService>();
            service.AddScoped<IJwtTokenService, JwtTokenService>();
            service.AddScoped<IIdEncoderDecoder, IdEncoderDecoder>();
            service.AddScoped<IAesCryptoService, AesCryptoService>();
            service.AddScoped<IProjectService, ProjectService>();

            
            /*Repos------------------------------------------------------*/
            service.AddSingleton(typeof(IBaseRepository<>),typeof(BaseRepository<>));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IProjectRepository, ProjectRepository>();

            return service;
        }
    }
}