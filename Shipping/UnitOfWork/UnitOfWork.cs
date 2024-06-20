using Shipping.Models;
using Shipping.Repository;
using Shipping.Repository.MerchantRepository;

namespace Shipping.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        ShippingContext db;
        IRepository<T> repo;
        IMerchantRepository merchantRepository;

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

        public IMerchantRepository MerchantRepository
        {
            get
            {
                if (merchantRepository == null) { merchantRepository = new MerchantRepository(db); }
                return merchantRepository;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
