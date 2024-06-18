using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Request;
using Jira.Response;

namespace Jira.Services
{
    public interface IProjectService
    {
        ProjectCreatedResponse HandleCreateProject(CreateProjectRequest createProjectRequest, int userId);
        List<ProjectCreatedResponse> HandleGetProjects(int userId);
        ProjectCreatedResponse HandleGetProjectById(int userId,  int projectId);
        void HandleDeactivateProject(int userId, int projectId);
    }
}