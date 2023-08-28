using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Services;
using RabbitMQ.Utils;

namespace PublishMessage.API.Services;

public class SendMessageService : ISendMessageService
{
    private readonly IRabbitMQPublishManager _rabbitMQPublishManager;
    private readonly IRabbitMQChannelManager _channelManager;

    public SendMessageService(IRabbitMQPublishManager rabbitMQPublishManager, IRabbitMQChannelManager channelManager)
    {
        _rabbitMQPublishManager = rabbitMQPublishManager;
        _channelManager = channelManager;
    }

    public void SendMessageAsync()
    {
        PublishMessage();
    }

    private void PublishMessage()
    {
        using var channel = _channelManager.GetChannel();
        _rabbitMQPublishManager.CreateExchange(channel, ExchangeObjects.SampleExchange);
        _rabbitMQPublishManager.DeclareQueue(channel, QueueObjects.SampleQueue);
        _rabbitMQPublishManager.BindQueue(channel, QueueObjects.SampleQueue, ExchangeObjects.SampleExchange);

        for (int i = 0; i < 100; i++)
        {
            var message = $"{i}";
            _rabbitMQPublishManager.Publish(channel, ExchangeObjects.SampleExchange.Name, QueueObjects.SampleQueue.RoutingKey, message, null);
        }
    }
}