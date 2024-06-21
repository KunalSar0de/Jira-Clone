using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.EFCore;
using Jira.Models;

namespace Jira.Repository.Impl
{
    public class TeamRepository : BaseRepository<Team>
    {
        private readonly JiraDbContext _dbContext;
        public TeamRepository(JiraDbContext jiraDbContext) : base(jiraDbContext)
        {
            _dbContext = jiraDbContext;
        }
    }
}