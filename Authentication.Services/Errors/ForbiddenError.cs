using Primitives;

namespace Authentication.Services.Errors;

public class ForbiddenError : ApplicationError
{
  public ForbiddenError(
    string message = "Ação proIbida", 
    string code = "FORBIDDEN", 
    int statusCode = 403
  ) : base(message, code, statusCode)
  {
  }
}