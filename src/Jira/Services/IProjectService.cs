using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models;
using Jira.Request;
using Jira.Response;

namespace Jira.Services
{
    public interface IProjectService
    {
        ProjectCreatedResponse HandleCreateProject(CreateProjectRequest createProjectRequest, User user);
        List<ProjectCreatedResponse> HandleGetProjects(int userId);
        ProjectCreatedResponse HandleGetProjectById(int userId,  int projectId);
        void HandleDeactivateProject(int userId, int projectId);
    }
}