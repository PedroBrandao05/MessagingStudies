using Primitives;

namespace Authentication.Infra.Security.Errors;

public class CouldNotRefreshToken : ApplicationError
{
  public CouldNotRefreshToken(
    string message = "Não foi possível fazer login automático", 
    string code = "COULDNT_REFRESH_TOKEN", 
    int statusCode = 400
  ) : base(message, code, statusCode)
  {
  }
}