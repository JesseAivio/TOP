using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TOP.API.Service
{
    public interface IModelService<TEntity>
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity Get(Guid Id);
        TEntity GetByName(string name);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
