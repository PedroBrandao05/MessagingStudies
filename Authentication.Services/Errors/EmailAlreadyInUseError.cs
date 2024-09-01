using Primitives;

namespace Authentication.Services.Errors;

public class EmailAlreadyInUseError : ApplicationError
{
  public EmailAlreadyInUseError(
    string message = "Usuário já cadastrado", 
    string code = "EMAIL_ALREADY_IN_USE", 
    int statusCode = 400
  ) : base(message, code, statusCode)
  {
  }
}