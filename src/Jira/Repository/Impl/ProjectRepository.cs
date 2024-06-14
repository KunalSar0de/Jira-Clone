using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.EFCore;
using Jira.Models;

namespace Jira.Repository.Impl
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly JiraDbContext _dbContext;
        public ProjectRepository(JiraDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}