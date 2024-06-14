using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jira.EFCore
{
    public class JiraDbContext : DbContext
    {
        public JiraDbContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JiraDbContext).Assembly);
        }

    }
}