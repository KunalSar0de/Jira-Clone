using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Request
{
    public class ResetPasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}