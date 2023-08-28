using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Services;

public class RabbitMQPublishManager : RabbitMQBaseManager , IRabbitMQPublishManager
{
    public void Publish(IModel model, string exchangeName, string routingKey, object body, IBasicProperties properties=null)
    {
        byte[] bodyBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body));
        model.BasicPublish(
        exchange: exchangeName,
        routingKey: routingKey,
        basicProperties: properties,
        body: bodyBytes);
    }
}