using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models;
using Microsoft.AspNetCore.Http;

namespace Jira.Services.Impl
{
    public class TokenHelperService : ITokenHelperService
    {
        private readonly IHttpContextAccessor _httpContext;

        public TokenHelperService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;

        }

        public User GetUserDetails()
        {
            var user = (User)_httpContext.HttpContext.Items["User"];
            if(user != null)
            {
                var userDetails = new User{
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    ProjectId = user.ProjectId,
                    IsActive = user.IsActive
                };

                return userDetails;
            }
            return null;
        }
    }
}