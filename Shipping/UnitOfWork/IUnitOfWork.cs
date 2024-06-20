using Shipping.Repository;
using Shipping.Repository.DeliveryRepo;

namespace Shipping.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        public IRepository<T> Repository { get; }

        public IDeliveryRepository DeliveryRepository { get; }
        public void SaveChanges();
    }
}
