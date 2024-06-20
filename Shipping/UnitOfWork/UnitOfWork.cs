using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shipping.Models;
using Shipping.Repository;
using Shipping.Repository.Employee_Repository;
using Shipping.Repository.DeliveryRepo;

namespace Shipping.UnitOfWork
{

    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        ShippingContext db;
        IRepository<T> repo;
        IDeliveryRepository deliveryRepository;
        private readonly UserManager<AppUser> userManager;
         IEmployeeRepository employeeRepository;

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

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }

}
