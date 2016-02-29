using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Jogging.Web.Infrastructure;
using Jogging.Web.Interfaces;

namespace Jogging.Web.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public abstract T Get(int id);
        public abstract void Update(T item);
        public abstract IEnumerable<T> GetAll();
        public abstract IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        public abstract T Add(T item);
        public abstract void Remove(int id);
    }
}