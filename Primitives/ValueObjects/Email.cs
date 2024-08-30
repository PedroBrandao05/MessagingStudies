using System.Net.Mail;
using Primitives.Errors;

namespace Primitives.ValueObjects;

public class Email
{
  public string Value { get; } = String.Empty;

  public static Email Empty()
  {
    return new Email();
  }

  public Email(string value)
  {
    Value = value;
    
    Validate();
  }
  
  private Email()
  {
  }

  public void Validate()
  {
    try
    {
      new MailAddress(Value);
    }
    catch (Exception e)
    {
      throw new InvalidEmailError();
    }
  }
}