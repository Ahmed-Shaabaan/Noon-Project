namespace ITI.E_Commerce.Api.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);

        TEntity GetByName(string name);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(int id);

        IEnumerable<TEntity> GetAll();


    }
}
