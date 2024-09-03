using Authentication.Services.Contracts;
using Authentication.Services.Contracts.Payloads;
using Authentication.Services.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controller;

[Route("auth")]
public class AuthController : ControllerBase
{
  private IAuthService _authService { get; }

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("signup")]
  public async Task<ActionResult> SignUp([FromBody] SignUpPayload payload)
  {
    await _authService.SignUp(new SignUpRequestPayload(payload));

    return Created();
  }
  
  [HttpPost("signin")]
  public async Task<ActionResult<SessionView>> SignIn([FromBody] SignInPayload payload)
  {
    var session = await _authService.SignIn(new SignInRequestPayload(payload));

    return Ok(session);
  }
  
  [HttpPost("refresh-token")]
  public async Task<ActionResult<SessionView>> RefreshToken([FromBody] RefreshTokenPayload payload)
  {
    var session = await _authService.RefreshToken(new RefreshTokenRequestPayload(payload));

    return Ok(session);
  }

  [HttpGet("request-admin-access")]
  [Authorize]
  public async Task<ActionResult> RequestAdminAccess()
  {
    var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    await _authService.RequestAdminAccess(
      new RequestAdminAccessRequestPayload(new RequestAdminAccessPayload(token))
    );

    return Ok();
  }
}