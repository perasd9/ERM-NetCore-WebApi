using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ILogic<TEntity> where TEntity : class
    {
        public Task<List<string>?> Add(TEntity entity);
        public Task Delete(object id);
        public Task<List<string>?> Update(TEntity entity);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> GetById(object id);
    }
}
