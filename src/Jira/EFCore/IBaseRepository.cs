using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jira.EFCore
{
    public interface IBaseRepository <T> where T : class
    {
        T Add (T entity);
        T GetById(int id);
        IReadOnlyList<T> GetAll();
        void Update(T entity);
        void Delete(T entity); 
    }
}