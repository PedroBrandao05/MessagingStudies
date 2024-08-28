using System.Net;
using Newtonsoft.Json;
using Primitives;

namespace Api.Middlewares;

public class GlobalErrorHandlingMiddleware 
{
  private readonly RequestDelegate _next;

  public GlobalErrorHandlingMiddleware(RequestDelegate next)
  {
    _next = next;
  }
  
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (ApplicationError ex)
    {
      await HandleExceptionAsync(context, ex);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  private static Task HandleExceptionAsync(HttpContext context, ApplicationError exception)
  {
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = exception.StatusCode;

    var response = new
    {
      statusCode = exception.StatusCode,
      message = exception.Message,
      code = exception.Code
    };

    var jsonResponse = JsonConvert.SerializeObject(response);

    return context.Response.WriteAsync(jsonResponse);
  }
  
  private static Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = 500;

    var response = new
    {
      statusCode = 500,
      message = exception.Message,
      code = "INTERNAL_SERVER_ERROR"
    };

    var jsonResponse = JsonConvert.SerializeObject(response);

    return context.Response.WriteAsync(jsonResponse);
  }
}