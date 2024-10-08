namespace Primitives;

public class ApplicationError : Exception
{
  public int StatusCode { get; }
  
  public string Code { get; }

  public ApplicationError(string message, string code, int statusCode) : base(message)
  {
    StatusCode = statusCode;

    Code = code;
  }
}