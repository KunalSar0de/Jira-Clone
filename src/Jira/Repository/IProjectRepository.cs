using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.EFCore;
using Jira.Models;

namespace Jira.Repository
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        bool IsProjectNameAvailable(string projectName, int userId);
        List<Project> GetAllProjectsBasedOnCreaatedBy(int userId);
        Project GetProjectsBasedOnId(int userId, int projectId);    
    }
}