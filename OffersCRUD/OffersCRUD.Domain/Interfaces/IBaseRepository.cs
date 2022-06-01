using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OffersCRUD.Domain
{
    public interface IBaseRepository<TEntity>  where TEntity : BaseEntity
    {
        List<TEntity> Get();
        TEntity GetById(int Id);
         bool Any(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int Id);
        void Save();
    }
}
