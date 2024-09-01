using System.Security.Cryptography;

namespace Authentication.Infra.Cryptography;

public class Encryptor
{
  public string Encrypt(string given)
  {
    byte[] salt;
    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

    var pbkdf2 = new Rfc2898DeriveBytes(given, salt, 10000);
    byte[] hash = pbkdf2.GetBytes(20);

    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 20);

    string hashedPassword = Convert.ToBase64String(hashBytes);

    return hashedPassword;
  }

  public bool Compare(string given, string expected)
  {
    byte[] hashBytes = Convert.FromBase64String(expected);

    byte[] salt = new byte[16];
    Array.Copy(hashBytes, 0, salt, 0, 16);

    var pbkdf2 = new Rfc2898DeriveBytes(given, salt, 10000);
    byte[] hash = pbkdf2.GetBytes(20);

    for (int i = 0; i < 20; i++) 
      if (hashBytes[i + 16] != hash[i])
        return false;

    return true;
  }
}