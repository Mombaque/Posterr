namespace Domain.Core.Repository
{
    public interface IRepository<TEntity, TID> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(TID id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
    }
}
