using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace KFC_Clone.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(object id);

        void Save();

        DbChangeTracker Table { get; }

        IQueryable<T> TableNoTracking { get; }

    }
}