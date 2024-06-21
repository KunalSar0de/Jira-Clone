using Jira.EFCore;
using Jira.Models;

namespace Jira.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByEmail(string email);
        bool IsUserFound(string emailId);
        void UpdateProjectId(int userId, int projectId);
    }
}