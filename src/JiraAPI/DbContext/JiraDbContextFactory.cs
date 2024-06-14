using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JiraAPI.DbContext
{
    public class JiraDbContextFactory : IDesignTimeDbContextFactory<JiraDbContext>
    {
        public JiraDbContext CreateDbContext(string[] args)
        {
            var configs = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var optionBuilder = new DbContextOptionsBuilder();

            string connectionString = configs.GetConnectionString("JiraDatabase");

            optionBuilder
                .UseMySql(
                    connectionString, 
                    ServerVersion.AutoDetect(connectionString), 
                    x => x.MigrationsAssembly("JiraAPI")
                );

            return new JiraDbContext(optionBuilder.Options);

        }
    }
}