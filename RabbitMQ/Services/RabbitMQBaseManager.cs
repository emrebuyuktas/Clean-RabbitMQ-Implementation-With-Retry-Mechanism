using RabbitMQ.Client;
using RabbitMQ.Internals;
using RabbitMQ.Models;

namespace RabbitMQ.Services;

public abstract class RabbitMQBaseManager : IRabbitMQBaseContract
{
    public void BindQueue(IModel channel, QueueConfig queueConfig, ExchangeConfig exchangeConfig)
    {
        Queue.Bind(channel, queueConfig, exchangeConfig);
    }

    public void CreateExchange(IModel channel, ExchangeConfig exchangeConfig)
    {
        Exchange.Create(channel, exchangeConfig);
    }

    public void DeclareQueue(IModel channel, QueueConfig queueConfig)
    {
        Queue.Declare(channel, queueConfig);
    }
}