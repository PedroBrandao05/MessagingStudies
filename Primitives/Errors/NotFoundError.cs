namespace Primitives.Errors;

public class NotFoundError : ApplicationError
{
  public NotFoundError(
    string message = "Not found", 
    string code = "NOT_FOUND", 
    int statusCode = 404
  ) : base(message, code, statusCode)
  {
  }
}