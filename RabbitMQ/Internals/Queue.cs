using RabbitMQ.Client;
using RabbitMQ.Models;

namespace RabbitMQ.Internals;

internal class Queue
{
    public static void Declare(IModel model, QueueConfig config)
    {
        model.QueueDeclare(queue: config.Name, durable: config.Durable, exclusive: config.Exclusive, autoDelete: config.AutoDelete, arguments: config.Arguments);
    }

    public static void Bind(IModel model, QueueConfig queueConfig, ExchangeConfig exchangeConfig)
    {
        model.QueueBind(queueConfig.Name, exchangeConfig.Name, queueConfig.RoutingKey);
    }

    public static void Publish(IModel model, string exchangeName, string routingKey, byte[] body, IBasicProperties properties)
    {
        model.BasicPublish(
            exchange: exchangeName,
            routingKey: routingKey,
            basicProperties: properties,
            body: body
        );
    }
}