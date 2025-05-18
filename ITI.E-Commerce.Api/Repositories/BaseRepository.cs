using Microsoft.EntityFrameworkCore;

namespace ITI.E_Commerce.Api.Repositories
{
    public class BaseRepository<TEntity, IDbContext> : IRepository<TEntity>
        where TEntity : class
        where IDbContext : DbContext
    {
        private readonly IDbContext dbContext;

        public BaseRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public TEntity Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return entity;

            dbContext.Set<TEntity>().Remove(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public TEntity GetById(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public TEntity GetByName(string name)
        {
            return dbContext.Set<TEntity>().Find(name);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public TEntity Update(TEntity entity)
        {

            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
            return entity;
        }
        //public IEnumerable<TEntity> Best_Seller()
        //{
        //    return dbContext.Set<TEntity>().ToList();
            
        //}
    }
}
