using Shipping.Repository;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using Shipping.Repository.OrderRepo;

namespace Shipping.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        public IRepository<T> Repository { get; }

        public IOrderRepository OrderRepository { get; }
        public ICityRepository CityRepository{ get; }
        public IGovernmentRepository GovernmentRepository  { get; }
        public void SaveChanges();

    }
}
