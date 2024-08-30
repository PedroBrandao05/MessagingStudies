namespace Primitives.Errors;

public class InvalidEmailError : ApplicationError
{
  public InvalidEmailError(
    string message = "Email inválido", 
    string code = "INVALID_EMAIL", 
    int statusCode = 400
  ) : base(message, code, statusCode)
  {
  }
}