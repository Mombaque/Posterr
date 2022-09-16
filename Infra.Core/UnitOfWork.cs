using Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Core
{
    public class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T _context;

        public UnitOfWork(T context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
