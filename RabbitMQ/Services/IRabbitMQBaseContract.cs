using RabbitMQ.Client;
using RabbitMQ.Models;

namespace RabbitMQ.Services;

public interface IRabbitMQBaseContract
{
    public void BindQueue(IModel channel, QueueConfig queueConfig, ExchangeConfig exchangeConfig);

    public void CreateExchange(IModel channel, ExchangeConfig exchangeConfig);

    public void DeclareQueue(IModel channel, QueueConfig queueConfig);
}
