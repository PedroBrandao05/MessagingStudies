namespace Primitives;

public class ApplicationError : Exception
{
  public int StatusCode { get; set; }
  
  public string Code { get; set; }

  public ApplicationError(string message, string code, int statusCode) : base(message)
  {
    StatusCode = statusCode;

    Code = code;
  }
}