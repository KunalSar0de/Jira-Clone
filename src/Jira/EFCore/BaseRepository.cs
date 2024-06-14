using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Jira.EFCore
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly JiraDbContext _jiraDbContext;

        public BaseRepository(JiraDbContext jiraDbContext)
        {
            _jiraDbContext = jiraDbContext;
        }

        public T Add(T entity)
        {
            _jiraDbContext.Set<T>().Add(entity);
            _jiraDbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _jiraDbContext.Set<T>().Remove(entity);
            _jiraDbContext.SaveChanges();
        }

        public IReadOnlyList<T> GetAll()
        {
            return _jiraDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _jiraDbContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _jiraDbContext.Entry(entity).State = EntityState.Modified;
            _jiraDbContext.SaveChanges();
        }
    }
}