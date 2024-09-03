using System.Text;
using Api.Middlewares;
using Authentication.Infra.Cryptography;
using Authentication.Infra.Database;
using Authentication.Infra.Repositories;
using Authentication.Infra.Repositories.Contracts;
using Authentication.Infra.Security;
using Authentication.Infra.Security.Contracts;
using Authentication.Services;
using Authentication.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Api;


public class Startup
{
  public IConfiguration Configuration { get; }
  
  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddDbContext<DbContext, AuthenticationDbContext>(cfg =>
    {
      cfg.UseMySql(
        Configuration.GetConnectionString("MYSQL_CONNECTION_STRING"),
        new MySqlServerVersion(new Version(8, 0, 21)));
    });

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IAdminRepository, AdminRepository>();
    services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    services.AddSingleton<Encryptor>();
    services.AddScoped<IJwt, Jwt>();
    services.AddScoped<IRefreshToken, RefreshTokenService>();

    services.AddScoped<IAuthService, AuthService>();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    
    var key = Encoding.ASCII.GetBytes(Configuration["JWT_SECRET"]);

    services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          ClockSkew = TimeSpan.Zero
        };
      });
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autenticação"));
    }

    app.UseMiddleware<GlobalErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
    });
    
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
      var dbContext = serviceScope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
      dbContext.Database.Migrate();
    }
  }
}