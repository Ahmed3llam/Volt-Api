using Shipping.Models;
using Shipping.Repository;

namespace Shipping.UnitOfWork
{

    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        ShippingContext db;
        IRepository<T> repo;


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

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }

}
