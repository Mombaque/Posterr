namespace Domain.Core.Repository
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
