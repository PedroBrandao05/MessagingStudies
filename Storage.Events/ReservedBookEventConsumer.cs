using Events;
using MassTransit;
using Primitives.Errors;
using Storage.Infra.Database;

namespace Storage.Events;

public class ReservedBookEventConsumer : IConsumer<ReservedBookEvent>
{
  private StorageDbContext _dbContext { get; set; }
  
  public ReservedBookEventConsumer(StorageDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  
  public async Task Consume(ConsumeContext<ReservedBookEvent> context)
  {
    var registry = _dbContext.Registries.Where(r => r.BookId == context.Message.BookId).ToList().FirstOrDefault();

    if (registry is null)
    {
      throw new NotFoundError();
    }

    registry.Quantity = Math.Max(0, registry.Quantity - 1);

    _dbContext.Registries.Update(registry);
    
    await _dbContext.SaveChangesAsync();
  }
}