namespace Primitives;

public abstract class DatabaseModel<TEntity> where TEntity : Entity
{
  public Guid Id { get; set; }
  public abstract TEntity ToEntity();

  public abstract DatabaseModel<TEntity> FromEntity(TEntity entity);
}