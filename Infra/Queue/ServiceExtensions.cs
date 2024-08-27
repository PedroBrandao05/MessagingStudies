using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Queue;

public static class ServiceExtensions
{
  public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
  {
    var host = Environment.GetEnvironmentVariable("RABBITMQ_HOSTNAME");
    var username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
    var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");
    
    services.AddMassTransit(config =>
    {
      config.UsingRabbitMq((context, cfg) =>
      {
        cfg.Host(host, "/", h =>
        {
          h.Username(username);
          h.Password(password);
        });
      });
    });

    services.AddMassTransitHostedService();

    return services;
  }
}