using Api.Middlewares;
using Infra.Queue;
using Microsoft.EntityFrameworkCore;
using Reservation.Infra.Database;
using Reservation.Infra.Repositories;
using Reservation.Infra.Repositories.Contracts;
using Reservation.Services;
using Reservation.Services.Contracts;

namespace Reservation.Api;

public class Startup
{
  public IConfiguration Configuration { get; }
  
  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddDbContext<DbContext, ReservationDbContext>(cfg =>
    {
      cfg.UseMySql(
        Configuration.GetConnectionString("MYSQL_CONNECTION_STRING"),
        new MySqlServerVersion(new Version(8, 0, 21)));
    });

    services.AddTransient<IReservationRepository, ReservationRepository>();
    services.AddTransient<IReservationService, ReservationService>();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddMassTransitWithRabbitMq();
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservas"));
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
      var dbContext = serviceScope.ServiceProvider.GetRequiredService<ReservationDbContext>();
      dbContext.Database.Migrate();
    }
  }
}