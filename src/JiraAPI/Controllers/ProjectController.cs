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
        private readonly IIdEncoderDecoder _idEncoderDecoder;

        public ProjectController
        (
            ITokenHelperService tokenHelperService,
            IProjectService projectService,
            IIdEncoderDecoder idEncoderDecoder)
        : base(tokenHelperService)
        {
            _projectService = projectService;
            _idEncoderDecoder = idEncoderDecoder;
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

        [HttpGet]
        [Route("project")]
        public IActionResult GetAllProjects()
        {
            //ToDo : Implement pegination
            var user = GetUserDetails();

            var allProjectResponse = _projectService.HandleGetProjects(user.Id);

            return Ok(allProjectResponse);
        }


        [HttpGet]
        [Route("project/{projectId}")]
        public IActionResult GetProjectById([FromRoute] string projectId)
        {
            if (string.IsNullOrWhiteSpace(projectId))
                return BadRequest();

            var userId = GetUserDetails().Id;

            var parsedProjectId = _idEncoderDecoder.DecodeId(projectId);
            if (parsedProjectId == 0)
                return BadRequest();

            var getByIdResponse = _projectService.HandleGetProjectById(userId, parsedProjectId);

            return Ok(getByIdResponse);
        }



        [HttpDelete]
        [Route("project/{projectId}")]
        public IActionResult DeleteProject([FromRoute] string projectId)
        {

            if (string.IsNullOrWhiteSpace(projectId))
                return BadRequest();

            var userId = GetUserDetails().Id;

            var parsedProjectId = _idEncoderDecoder.DecodeId(projectId);
            if (parsedProjectId == 0)
                return BadRequest();

            _projectService.HandleDeactivateProject(userId, parsedProjectId);
            
            return Ok();
        }

    }
}