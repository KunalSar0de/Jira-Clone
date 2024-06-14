using Jira.EFCore;
using Jira.Models;

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
            return _dbContext.User.FirstOrDefault(x => x.Email == email);
        }

        public bool IsUserFound(string emailId)
        {
            return _dbContext.User.Any(x=>x.Email == emailId);
        }
    }
}