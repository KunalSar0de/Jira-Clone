using Jira.EFCore;
using Jira.Models;
using Microsoft.EntityFrameworkCore;

namespace Jira.Repository.Impl
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly JiraDbContext _dbContext;
        public UserRepository(JiraDbContext jiraDbContext) : base(jiraDbContext)
        {
            _dbContext = jiraDbContext;
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.User.Where(x => x.Email == email).Include(x=>x.Project).FirstOrDefault();
        }

        public bool IsUserFound(string emailId)
        {
            return _dbContext.User.Any(x=>x.Email == emailId);
        }

        public void UpdateProjectId(int userId, int projectId)
        {
            var user = new User {Id = userId, ProjectId = projectId };
            _dbContext.User.Attach(user).Property(x=>x.ProjectId).IsModified = true;
            _dbContext.SaveChanges();
        }
    }
}