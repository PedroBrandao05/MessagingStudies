namespace Authentication.Services.Contracts.Responses;

public class SessionView
{
  public UserView User { get; set; }

  public string Token { get; set; } = String.Empty;
  
  public string RefreshToken { get; set; } = String.Empty;
}