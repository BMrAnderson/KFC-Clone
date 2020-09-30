using System;
using System.Collections.Generic;
using System.Linq;
using KFC_Clone.Models.DBModels;
using System.Web;
using System.Data.Entity;
using KFC_Clone.Logging;
using System.Data.Entity.Infrastructure;

namespace KFC_Clone.Models.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private KFCDBEntities _context;
        private DbSet<T> _table;
        private ILogging _logger;

        public DbChangeTracker Table { get; }
        public IQueryable<T> TableNoTracking { get; }


        public Repository(KFCDBEntities context, ILogging logging )
        {
            _context = context;
           
            _table = _context.Set<T>();
            
            TableNoTracking = _context.Set<T>().AsNoTracking();
            
            Table = _context.ChangeTracker;
            
            _logger = logging;
        }

        public void Delete(object id)
        {
            var entity = _table.Find(id);
            
            if (entity != null)
            {
                _table.Remove(entity);
                
                _context.SaveChanges();
                
                Log();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList<T>();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                _table.Add(entity);
                
                _context.SaveChanges();
                
                Log();
            }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                _table.Attach(entity);
                
                _context.Entry(entity).State = EntityState.Modified;
                
                Log();
            }
        }

        private void Log()
        {
            _logger.Log($"Entities tracked: {Table.Entries().Count()}");
        }

        public void Insert(IEnumerable<T> entities)
        {
            _table.AddRange(entities);
           
            _context.SaveChanges();
            
            Log();
        }

        public void Save()
        {
            _context.SaveChanges();

            Log();
        }
    }
}