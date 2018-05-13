using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySocNet.Dal.Entities;

namespace MySocNet.Dal.Abstract
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        ICollection<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
