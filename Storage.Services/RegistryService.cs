using Storage.Entities;
using Storage.Infra.Repositories.Contracts;
using Storage.Services.Contracts;

namespace Storage.Services;

public class RegistryService : IRegistryService
{
  private IRegistryRepository _registryRepository { get; }

  public RegistryService(IRegistryRepository registryRepository)
  {
    _registryRepository = registryRepository;
  }

  public async Task RegisterBook()
  {
    var registry = new Registry
    {
      BookId = Guid.NewGuid(),

      Quantity = 4
    };

    await _registryRepository.Create(registry);
  }
}