using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Repositories.Repository
{
    interface IRepository
    {
        List<T> GetAll<T>(bool withTracking = false) where T : class;

        List<T> GetAllWithPaging<T>(int skip, int take, bool withTracking = false) where T : class;

        IQueryable<T> GetAllWhereQ<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;

        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Remove<T>(T entity) where T : class;

        int Count<T>() where T : class;

        bool SaveChangesAsync();
    }
}
