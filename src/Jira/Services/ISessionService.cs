using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Request;
using Jira.Response;

namespace Jira.Services
{
    public interface ISessionService
    {
        LoginResponse HandleLoginSession(LoginRequest loginRequest);
        UserCreatedResponse HandleCreateUser(RegisterUserRequest registerUserRequest);
        void HandleResetPassword(ResetPasswordRequest resetPasswordRequest, int userId);
    }
}