using Microsoft.AspNetCore.Mvc;
using Storage.Services.Contracts;

namespace Storage.Api.Controllers;

[Route("registry")]
public class RegistryController : ControllerBase
{
  private IRegistryService _registryService { get; }

  public RegistryController(IRegistryService registryService)
  {
    _registryService = registryService;
  }

  [HttpPost]
  public async Task<ActionResult> RegisterBook()
  {
    await _registryService.RegisterBook();

    return Created();
  }
}