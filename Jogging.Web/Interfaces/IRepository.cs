using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Jogging.Web.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get(int id);
        void Update(T item);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T Add(T item);
        void Remove(int id);
    }
}