using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task Add(TEntity entity);
        public Task Delete(object id);
        public Task Update(TEntity entity);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetById(object id);
    }
}
