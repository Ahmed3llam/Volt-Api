using Shipping.Repository;
using Shipping.Repository.MerchantRepository;

namespace Shipping.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        IRepository<T> Repository { get; }
        IMerchantRepository MerchantRepository { get; }
        void SaveChanges();
    }
}
