using Primitives;
using Storage.Entities;

namespace Storage.Infra.Database.Models;

public class RegistryModel : DatabaseModel<Registry>
{
  public Guid BookId { get; set; }
  
  public int Quantity { get; set; }
  
  public override Registry ToEntity()
  {
    return new Registry
    {
      Id = Id,

      BookId = BookId,

      Quantity = Quantity
    };
  }

  public override RegistryModel FromEntity(Registry entity)
  {
    return new RegistryModel
    {
      Id = entity.Id,
      
      BookId = entity.BookId,
      
      Quantity = entity.Quantity
    };
  }
}