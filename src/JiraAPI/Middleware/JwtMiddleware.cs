using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Repository;
using Jira.Services;

namespace JiraAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtTokenService.ValidateJwtToken(token);

            if (userId != null)
            {
                context.Items["User"] = userRepository.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}