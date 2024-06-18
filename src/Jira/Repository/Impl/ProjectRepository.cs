using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.EFCore;
using Jira.Models;
using Microsoft.EntityFrameworkCore;

namespace Jira.Repository.Impl
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly JiraDbContext _dbContext;
        public ProjectRepository(JiraDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Project> GetAllProjectsBasedOnCreaatedBy(int userId)
        {
            return _dbContext.Project
                    .Where(x => x.CreatedBy == userId)
                    .AsNoTracking()
                    .ToList();
        }

        public Project GetProjectsBasedOnId(int userId, int projectId)
        {
            return _dbContext.Project
                    .FirstOrDefault(x => x.CreatedBy == userId && x.Id == projectId);
        }

        public bool IsProjectNameAvailable(string projectName, int userId)
        {
            return _dbContext.Project.Any(x => x.Name == projectName && x.CreatedBy == userId);
        }
    }
}