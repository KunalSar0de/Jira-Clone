using Jira.Models;
using Jira.Request;
using Jira.Response;

namespace Jira.Services
{
    public interface IJwtTokenService
    {
        JwtTokenCreationResponse CreateJwtToken(LoginRequest loginRequest, User user);
        int? ValidateJwtToken(string jwtToken);
    }
}