using Shipping.Repository;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.Employee_Repository;
using Shipping.Repository.MerchantRepository;
using Shipping.Repository;
using Shipping.Repository.MerchantRepository;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using Shipping.Repository.OrderRepo;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.Employee_Repository;
namespace Shipping.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        public IRepository<T> Repository { get; }

        public IDeliveryRepository DeliveryRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IMerchantRepository MerchantRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public ICityRepository CityRepository { get; }
        public IGovernmentRepository GovernmentRepository { get; }
        public IDeliveryRepository DeliveryRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }

    
        public void SaveChanges();
    }


  
     

    }
