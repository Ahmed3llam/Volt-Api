using Shipping.Repository;

namespace Shipping.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        public IRepository<T> Repository { get; }
        public void SaveChanges();
    }
}
