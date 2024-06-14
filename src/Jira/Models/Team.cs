using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public bool IsActive { get; set; }
    }
}