using Primitives;

namespace Authentication.Services.Contracts.Payloads;

public record SignInPayload(
  string Email,
  
  string Password
);

public class SignInRequestPayload : RequestPayload<SignInPayload>
{
  public SignInRequestPayload(SignInPayload payload) : base(payload)
  {
  }

  public override void Validate()
  {
  }
}