using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        protected DateTime CreatedOn { get; set; }
        protected DateTime ModifiedOn { get; set; }
    }
}