using Primitives;

namespace Authentication.Services.Contracts.Payloads;

public record RefreshTokenPayload(
  string AccessToken
);

public class RefreshTokenRequestPayload : RequestPayload<RefreshTokenPayload>
{
  public RefreshTokenRequestPayload(RefreshTokenPayload payload) : base(payload)
  {
  }

  public override void Validate()
  {
  }
}