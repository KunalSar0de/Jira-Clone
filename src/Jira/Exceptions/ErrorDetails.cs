using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Exceptions
{
    public class ErrorDetails
    {
        public string ErrorCode { get; set; }
        public string  ErrorMessage { get; set; }
    }

    public class ErrorStackDetails
    {
        public string  ErrorMessage { get; set; }
        public string  StackTrace { get; set; }
    }
}