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
        
    }
}