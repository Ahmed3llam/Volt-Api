using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shipping.Models;
using Shipping.Repository;
using Shipping.Repository.CityRepo;
using Shipping.Repository.GovernmentRepo;
using Shipping.Repository.OrderRepo;
using Shipping.Repository.Employee_Repository;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.MerchantRepository;
using Shipping.Repository.BranchRepository;

namespace Shipping.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        ShippingContext db;
        IRepository<T> repo;
        IDeliveryRepository deliveryRepository;
        private readonly UserManager<AppUser> userManager;
        IEmployeeRepository employeeRepository;
        IMerchantRepository merchantRepository;
         IOrderRepository orderRepository;
        ICityRepository cityRepository;
        IGovernmentRepository governmentRepository;
        IBranchRepository branchRepository;
        private readonly ILoggerFactory _loggerFactory;


        public UnitOfWork(ShippingContext db, UserManager<AppUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;

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
         public IDeliveryRepository DeliveryRepository
        {
            get
            {
                if (deliveryRepository == null) { deliveryRepository = new DeliveryRepository(db , userManager); }
                return deliveryRepository;
            }
        }
          public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (employeeRepository == null) { employeeRepository = new EmployeeRepository(db); }
                return employeeRepository;
            }
        }
        public IMerchantRepository MerchantRepository
        {
            get
            {
                if (merchantRepository == null) { merchantRepository = new MerchantRepository(db); }
                return merchantRepository;
            }
        }
        public IBranchRepository BranchRepository
        {
            get
            {
                if (branchRepository == null) { branchRepository = new BranchRepository(db); }
                return branchRepository;
            }
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
