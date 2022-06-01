using Microsoft.EntityFrameworkCore;
using OffersCRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OffersCRUD.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OfferDbContext _context;
        protected  DbSet<TEntity> table;
        public BaseRepository(OfferDbContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();

        }
        public  void Add(TEntity entity)
        {
            table.Add(entity);
        }

        public  void Delete(int id)
        {
            var entiity = GetById(id);
            _context.Remove(entiity);
            Save();
        }

        public  List<TEntity> Get()
        {
            return  table.ToList();
            
        }

        public  TEntity GetById(int Id)
        {
            return  table.FirstOrDefault(x => x.Id == Id);
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            var result = table.Where(predicate).ToList();
            return result.Count()>1 ;
        }

        public  void Update(TEntity entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public  void Save()
        {
            _context.SaveChanges();
        }
    }

}
