using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shipping.Models;
using Shipping.Repository;
using Shipping.Repository.Employee_Repository;

namespace Shipping.UnitOfWork
{

    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        ShippingContext db;
        IRepository<T> repo;
        IEmployeeRepository employeeRepository;


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
