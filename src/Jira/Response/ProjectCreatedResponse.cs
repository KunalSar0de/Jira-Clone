using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Response
{
    public class ProjectCreatedResponse
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }

    }
}