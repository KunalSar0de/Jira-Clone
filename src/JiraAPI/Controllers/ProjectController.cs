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
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        public ProjectController
        (
            ITokenHelperService tokenHelperService,
            IProjectService projectService
        )
        : base(tokenHelperService)
        {
            _projectService = projectService;
        }


        [HttpPost]
        [Route("project")]
        public IActionResult CreateProject([FromBody] CreateProjectRequest createProjectRequest)
        {
            if (createProjectRequest == null)
                return BadRequest();

            var user = GetUserDetails();

            var projectCreatedResponse = _projectService.HandleCreateProject(createProjectRequest, user.Id);

            return Created("project", projectCreatedResponse);
        }


        [HttpPut]
        [Route("project")]
        public IActionResult UpdateProject([FromBody] CreateProjectRequest createProjectRequest)
        {
            if (createProjectRequest == null)
                return BadRequest();

            return Ok(createProjectRequest);
        }


        [HttpDelete]
        [Route("project")]
        public IActionResult DeleteProject()
        {


            return Ok();
        }

    }
}