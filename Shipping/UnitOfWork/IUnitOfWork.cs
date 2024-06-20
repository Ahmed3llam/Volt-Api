using Shipping.Repository;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.Employee_Repository;

namespace Shipping.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        public IRepository<T> Repository { get; }

        public IDeliveryRepository DeliveryRepository { get; }
                public IEmployeeRepository EmployeeRepository { get; }

        public void SaveChanges();
    }
}
