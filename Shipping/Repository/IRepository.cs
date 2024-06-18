using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shipping.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        
        public List<TEntity> Pagination(List<TEntity> List, int page = 1, int pageSize = 9);
       
        public List<TEntity> GetByTitle(string name, params Expression<Func<TEntity, object>>[] includeProperties);
        
        public bool Exist(int id);
        
        public TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includeProperties);
        


        public void Add(TEntity entity);
      
        public void Update(TEntity entity);
      
        public void Delete(int id);
        
        public void Save();
       


    }
}
