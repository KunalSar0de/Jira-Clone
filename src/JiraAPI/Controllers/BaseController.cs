using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models;
using Jira.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraAPI.Controllers
{
    [Consumes("application/json")]
    [Produces(contentType: "application/json")]
    public class BaseController : ControllerBase
    {
        private readonly ITokenHelperService _tokenHelperService;

        public BaseController(ITokenHelperService tokenHelperService)
        {
            _tokenHelperService = tokenHelperService;
        }

        public User GetUserDetails()  => _tokenHelperService.GetUserDetails();
        

    }
}