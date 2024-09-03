using Primitives;

namespace Authentication.Services.Contracts.Payloads;

public record RequestAdminAccessPayload(
  string Token
);

public class RequestAdminAccessRequestPayload : RequestPayload<RequestAdminAccessPayload>
{
  public RequestAdminAccessRequestPayload(RequestAdminAccessPayload payload) : base(payload)
  {
  }

  public override void Validate()
  {
  }
}