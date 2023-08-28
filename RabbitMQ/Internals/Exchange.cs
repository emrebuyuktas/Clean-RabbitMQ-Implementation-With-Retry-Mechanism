using RabbitMQ.Client;
using RabbitMQ.Models;

namespace RabbitMQ.Internals;

internal class Exchange
{
    public static void Create(IModel model, ExchangeConfig config)
    {
        model.ExchangeDeclare(exchange: config.Name, type: config.Type,durable:config.Durable,autoDelete:config.AutoDelete,arguments:config.Arguments);
    }
}