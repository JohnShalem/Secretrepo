using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using WhatsAppAPI.Data;

namespace WhatsAppAPI.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private  RegistrationDbContext _registrationDbContext;
        public GenericRepository(RegistrationDbContext registrationDbContext)
        {
            _registrationDbContext = registrationDbContext;
        }

        public TEntity Add(TEntity entity)
        {
            
            var Entity = _registrationDbContext.Add(entity).Entity;
            SaveChanges();
            return Entity;
        }

        public IQueryable<TEntity> All()
        {
            return _registrationDbContext.Set<TEntity>().ToList().AsQueryable();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _registrationDbContext.Set<TEntity>().AsQueryable().Where(predicate).ToList();

        }

        public TEntity Get(int id)
        {
            return _registrationDbContext.Find<TEntity>();
        }

        public void SaveChanges()
        {
            _registrationDbContext.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            var k =  _registrationDbContext.Update(entity).Entity;
            SaveChanges();
            return k;
        }

    }
}
