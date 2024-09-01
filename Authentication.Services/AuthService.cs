using Authentication.Entities;
using Authentication.Infra.Cryptography;
using Authentication.Infra.Repositories.Contracts;
using Authentication.Infra.Security.Contracts;
using Authentication.Infra.Security.Errors;
using Authentication.Services.Contracts;
using Authentication.Services.Contracts.Payloads;
using Authentication.Services.Contracts.Responses;
using Authentication.Services.Errors;
using Primitives.Errors;
using Primitives.ValueObjects;

namespace Authentication.Services;

public class AuthService : IAuthService
{
  private IUserRepository _userRepository { get; }
  
  private Encryptor _encryptor { get; }
  
  private IAdminRepository _adminRepository { get; }
  
  private IRefreshToken _refreshToken { get; }
  
  private IJwt _jwt { get; }

  public AuthService(IUserRepository userRepository, Encryptor encryptor, IJwt jwt)
  {
    _userRepository = userRepository;

    _encryptor = encryptor;

    _jwt = jwt;
  }
  
  public async Task SignUp(SignUpRequestPayload request)
  {
    var existant = await _userRepository.GetByEmail(request.Payload.Email);

    if (existant is not null)
    {
      throw new EmailAlreadyInUseError();
    }

    var user = new User
    {
      Email = new Email(request.Payload.Email),
      
      Name = request.Payload.Name,
      
      Password = _encryptor.Encrypt(request.Payload.Password)
    };

    await _userRepository.Create(user);
  }

  public async Task<SessionView> SignIn(SignInRequestPayload request)
  {
    var user = await _userRepository.GetByEmail(request.Payload.Email);
    var admin = await _adminRepository.GetByEmail(request.Payload.Email);

    if (user is null)
    {
      throw new NotFoundError("Usuário não cadastrado");
    }

    var passwordIsValid = _encryptor.Compare(request.Payload.Password, user.Password);

    if (!passwordIsValid)
    {
      throw new IncorrectCredentialsError();
    }

    var token = _jwt.GenerateToken(user, admin?.CompanyId);
    var refreshToken = await _refreshToken.GenerateRefreshToken(user.Id);

    var sessionView = new SessionView
    {
      User = new UserView
      {
        CompanyId = admin?.CompanyId,
        
        Email = user.Email.Value,
        
        Name = user.Name,
        
        Id = user.Id
      },
      
      Token = token,
      
      RefreshToken = refreshToken
    };

    return sessionView;
  }

  public async Task<SessionView> RefreshToken(RefreshTokenRequestPayload request)
  {
    var session = _jwt.Decode(request.Payload.AccessToken);

    await _refreshToken.ValidateRefreshToken(session.UserId);

    var user = await _userRepository.GetById(session.UserId);
    var admin = await _adminRepository.GetById(session.UserId);

    if (user is null)
    {
      throw new CouldNotRefreshToken();
    }
    
    var token = _jwt.GenerateToken(user, admin?.CompanyId);
    var refreshToken = await _refreshToken.GenerateRefreshToken(user.Id);

    var sessionView = new SessionView
    {
      User = new UserView
      {
        CompanyId = admin?.CompanyId,
        
        Email = user.Email.Value,
        
        Name = user.Name,
        
        Id = user.Id
      },
      
      Token = token,
      
      RefreshToken = refreshToken
    };

    return sessionView;
  }
}