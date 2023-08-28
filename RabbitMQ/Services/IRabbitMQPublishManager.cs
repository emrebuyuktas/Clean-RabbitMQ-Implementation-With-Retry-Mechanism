using RabbitMQ.Client;

namespace RabbitMQ.Services;

public interface IRabbitMQPublishManager : IRabbitMQBaseContract
{
    void Publish(IModel model, string exchangeName, string routingKey, object body, IBasicProperties properties);
}