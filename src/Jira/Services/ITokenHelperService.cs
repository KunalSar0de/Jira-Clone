using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models;

namespace Jira.Services
{
    public interface ITokenHelperService
    {
        User GetUserDetails();
    }
}