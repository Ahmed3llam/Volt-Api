using Shipping.Models;
using Shipping.Repository;

namespace Shipping.UnitOfWork
{
    public class Unit<TEntity> where TEntity : class
    {
        ShippingContext db;
        //Repos
        public Unit()
        {
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
