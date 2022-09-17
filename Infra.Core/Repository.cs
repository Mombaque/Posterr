using Domain.Core.Models;
using Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Db;
        protected DbSet<T> DbSet;

        public Repository(DbContext context)
        {
            Db = context;
            DbSet = context.Set<T>();
        }

        public void Add(T entity) => DbSet.Add(entity);

        public void Update(T entity) => DbSet.Update(entity);

        public void Delete(T entity) => DbSet.Remove(entity);

        public void SaveChanges() => Db.SaveChanges();

        public IQueryable<T> GetAll() => DbSet;

        public T Get(Guid id) => DbSet.Find(id);
    }
}