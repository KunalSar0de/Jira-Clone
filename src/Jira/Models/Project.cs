using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class Project
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }

    }
}