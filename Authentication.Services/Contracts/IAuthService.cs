using Authentication.Services.Contracts.Payloads;
using Authentication.Services.Contracts.Responses;

namespace Authentication.Services.Contracts;

public interface IAuthService
{
  public Task SignUp(SignUpRequestPayload request);

  public Task<SessionView> SignIn(SignInRequestPayload request);

  public Task<SessionView> RefreshToken(RefreshTokenRequestPayload request);
}