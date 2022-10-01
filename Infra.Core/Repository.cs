using Domain.Core.Models;
using Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Core
{
    public class Repository<TEntity, TID> : IRepository<TEntity, TID>, IDisposable where TEntity : class
    {
        protected DbContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Db = context;
            DbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity) => DbSet.Add(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public void SaveChanges() => Db.SaveChanges();

        public IQueryable<TEntity> GetAll() => DbSet;

        public virtual TEntity GetById(TID id) => DbSet.Find(id);

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}