using Primitives;

namespace Authentication.Services.Errors;

public class IncorrectCredentialsError : ApplicationError
{
  public IncorrectCredentialsError(
    string message = "Email ou senha incorretos",
    string code = "INCORRECT_CREDENTIALS", 
    int statusCode = 403
  ) : base(message, code, statusCode)
  {
  }
}