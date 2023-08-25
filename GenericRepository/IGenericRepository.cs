using System.Linq.Expressions;

namespace WhatsAppAPI.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);
            
        TEntity Get(int id);

        IQueryable<TEntity> All();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void SaveChanges();

    }
}
