using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Attributes;
using Jira.Request;
using Jira.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraAPI.Controllers
{


    public class SessionsController : BaseController
    {
        private readonly ISessionService _sessionService;

        public SessionsController(
            ITokenHelperService tokenHelperService,
            ISessionService sessionService
        ) 
        : base(tokenHelperService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("user/register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
        {
            if(registerUserRequest.Equals(null))
                return BadRequest();

            var user = _sessionService.HandleCreateUser(registerUserRequest);
            return Created("user/register",user);
        }  



        [HttpPost]
        [Route("user/login")]
        public IActionResult CreateSession([FromBody] LoginRequest loginRequest)
        {
            if(loginRequest == null)
                return BadRequest();

            var loginResponse = _sessionService.HandleLoginSession(loginRequest);
            return Ok(loginResponse);
        }

        [HttpPost]
        [Route("user/forgetpassword")]
        public IActionResult ForgetPass([FromBody] LoginRequest loginRequest)
        {
            if(loginRequest == null)
                return BadRequest();

            return Ok(loginRequest);
        }

    
        [Authorize]
        [HttpPut]
        [Route("user/resetpassword")]
        public IActionResult ResetPass([FromBody] ResetPasswordRequest resetPasswordRequest)
        {
            if(resetPasswordRequest == null)
                return BadRequest();

            var userId = GetUserDetails().Id;

            _sessionService.HandleResetPassword(resetPasswordRequest, userId);

            return Ok();
        }
        
    }
}