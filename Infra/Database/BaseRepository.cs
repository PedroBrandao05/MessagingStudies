using Microsoft.EntityFrameworkCore;
using Primitives;

namespace Infra.Database;

public abstract class BaseRepository<TEntity, TDbModel> : IRepository<TEntity> where TEntity : Entity where TDbModel : DatabaseModel<TEntity> 
{
  protected DbSet<TDbModel> _dbSet { get; set; }
  
  protected DbContext _dbContext { get; set; }

  public BaseRepository(DbContext dbContext)
  {
    _dbContext = dbContext;
    
    _dbSet = dbContext.Set<TDbModel>();
  }
  
  public Task<TEntity?> GetById(Guid id)
  {
    var e =  _dbSet.Where(e => e.Id == id).ToList().FirstOrDefault();

    if (e is null)
    {
      return Task.FromResult<TEntity?>(null);
    }
    
    return Task.FromResult<TEntity?>(e.ToEntity());
  }

  public Task<List<TEntity>> GetAll()
  {
    var entities = _dbSet.Where(e => true).ToList();
    return Task.FromResult(entities.Select(e => e.ToEntity()).ToList());
  }

  public async Task Create(TEntity entity)
  {
    _dbSet.Add(ToDatabaseModel(entity));

    await _dbContext.SaveChangesAsync();
  }

  public async Task Update(TEntity entity)
  {
    _dbSet.Update(ToDatabaseModel(entity));

    await _dbContext.SaveChangesAsync();
  }

  public async Task Delete(TEntity entity)
  {
    _dbSet.Remove(ToDatabaseModel(entity));

    await _dbContext.SaveChangesAsync();
  }

  protected abstract TDbModel ToDatabaseModel(TEntity entity);
}