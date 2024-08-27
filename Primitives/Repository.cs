namespace Primitives;

public interface IRepository<TEntity> where TEntity : Entity
{
  public Task<TEntity?> GetById(Guid id);

  public Task<List<TEntity>> GetAll();

  public Task Create(TEntity entity);

  public Task Update(TEntity entity);

  public Task Delete(TEntity entity);
}