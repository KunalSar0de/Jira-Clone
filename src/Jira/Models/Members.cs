using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class Members
    {
        public string Email { get; set; }
        public int TeamId { get; set; }
        public bool IsActive { get; set; }

    }
}