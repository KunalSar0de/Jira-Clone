using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Response
{
    public class JwtTokenCreationResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }

    }
}