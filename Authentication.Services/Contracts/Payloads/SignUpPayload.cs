using Primitives;
using Primitives.ValueObjects;

namespace Authentication.Services.Contracts.Payloads;

public record SignUpPayload(
  string Email,
  
  string Name,
  
  string Password
);

public class SignUpRequestPayload : RequestPayload<SignUpPayload>
{
  public override void Validate()
  {
    new Email(Payload.Email);
  }

  public SignUpRequestPayload(SignUpPayload payload) : base(payload)
  {
  }
}