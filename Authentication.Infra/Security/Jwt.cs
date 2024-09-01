using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Entities;
using Authentication.Infra.Security.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infra.Security;

public class Jwt : IJwt
{
  private readonly string _secret;

    public Jwt(IConfiguration configuration)
    {
        _secret = configuration["JWT_SECRET"] ?? throw new ArgumentNullException("JWT_SECRET");
    }

    public string GenerateToken(User user, Guid? companyId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                new Claim("companyId", companyId?.ToString() ?? string.Empty)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Session Decode(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
        var email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
        var companyIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "companyId")?.Value;
        var companyId = companyIdClaim != null ? Guid.Parse(companyIdClaim) : (Guid?)null;

        return new Session
        {
            UserId = userId,
            Email = email,
            CompanyId = companyId
        };
    }
}