using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Models;
using RabbitMQ.Services;

namespace RabbitMQ;

public static class RabbitMQServiceRegistration
{
    public static IServiceCollection AddRabbitMQServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region RabbitMQ
        services.AddSingleton(sp =>
        {
            var settings = configuration.GetSection("RabbitMQConfigurations").Get<RabbitMQSettings>();
            var factory = new ConnectionFactory
            {
                HostName = settings.HostName,
                Port = settings.Port,
                UserName = settings.UserName,
                Password = settings.Password,
                DispatchConsumersAsync = settings.DispatchConsumersAsync
            };

            return factory.CreateConnection();
        });
        services.AddSingleton<IRabbitMQChannelManager, RabbitMQChannelManager>();
        services.AddSingleton<IRabbitMQPublishManager, RabbitMQPublishManager>();
        services.AddSingleton<IRabbitMQConsumerManager, RabbitMQConsumerManager>();
        #endregion

        return services;
    }
}
