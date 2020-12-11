using CleanArc.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Repositories.Repository
{
    public class Repository : IRepository
    {
        CleanArchDBContext _context;

        public Repository(CleanArchDBContext context)
        {
            _context = context;
        }

        #region Private Methods

        private IQueryable<T> InsializeQuery<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            var query = _context.Set<T>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        #endregion

        #region Add

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        #endregion

        #region Count

        public int Count<T>() where T : class
        {
            return _context.Set<T>().Count();
        }

        #endregion

        #region Get Methods

        public IQueryable<T> GetAllWhereQ<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            return InsializeQuery<T>(includes).Where(predicate);
        }

        public List<T> GetAllWithPaging<T>(int skip, int take, bool withTracking) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().Skip(skip).Take(take).ToList();
            else
                return InsializeQuery<T>().Skip(skip).Take(take).AsNoTracking().ToList();
        }

        public List<T> GetAll<T>(bool withTracking = true) where T : class
        {
            if (withTracking)
                return InsializeQuery<T>().ToList();
            else
                return InsializeQuery<T>().AsNoTracking().ToList();
        }

        #endregion

        #region Remove

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        #endregion

        #region Update

        public void Update<T>(T entity) where T : class
        {
            _context.Attach(entity);
            _context.Update<T>(entity);
        }

        #endregion

        #region Save Changes

        public bool SaveChangesAsync()
        {
            return _context.SaveChanges() > 0;
        }

        #endregion

    }
}
