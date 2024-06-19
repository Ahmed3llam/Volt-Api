using Shipping.Models;
using Shipping.Repository;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using Shipping.Repository.OrderRepo;

namespace Shipping.UnitOfWork
{

    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        ShippingContext db;
        IRepository<T> repo;
        IOrderRepository orderRepository;
        ICityRepository cityRepository;
        IGovernmentRepository governmentRepository;

        public UnitOfWork(ShippingContext db)
        {
            this.db = db;
        }

        public IRepository<T> Repository
        {
            get
            {
                if (repo == null) { repo = new Repository<T>(db); }
                return repo;
            }
        }
        public IOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null) { orderRepository = new OrderRepository(db); }
                return orderRepository;
            }
        }
        public ICityRepository CityRepository
        {
            get
            {
                if (cityRepository == null) { cityRepository = new CityRepository(db); }
                return cityRepository;
            }
        }
        
        public IGovernmentRepository GovernmentRepository
        {
            get
            {
                if (governmentRepository == null) { governmentRepository = new GovernmentRepository(db); }
                return governmentRepository;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }

}
