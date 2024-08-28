using Api.Middlewares;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Storage.Events;
using Storage.Infra.Database;
using Storage.Infra.Repositories;
using Storage.Infra.Repositories.Contracts;
using Storage.Services;
using Storage.Services.Contracts;

namespace Storage.Api;

public class Startup
{
  public IConfiguration Configuration { get; }
  
  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddDbContext<DbContext, StorageDbContext>(cfg =>
    {
      cfg.UseMySql(
        Configuration.GetConnectionString("MYSQL_CONNECTION_STRING"),
        new MySqlServerVersion(new Version(8, 0, 21)));
    });

    services.AddTransient<IRegistryRepository, RegistryRepository>();
    services.AddTransient<IRegistryService, RegistryService>();

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddMassTransit(config =>
    {
      config.AddConsumer<ReservedBookEventConsumer>();

      var host = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME") ?? String.Empty;
      var username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? String.Empty;
      var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? String.Empty;

      config.UsingRabbitMq((context, cfg) =>
      {
        cfg.Host(host, "/", h =>
        {
          h.Username(username);
          h.Password(password);
        });

        cfg.ReceiveEndpoint("reserved-book-event-queue", ep =>
        {
          ep.ConfigureConsumer<ReservedBookEventConsumer>(context);
        });
      });
    });
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Armazém"));
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
      var dbContext = serviceScope.ServiceProvider.GetRequiredService<StorageDbContext>();
      dbContext.Database.Migrate();
    }
  }
}