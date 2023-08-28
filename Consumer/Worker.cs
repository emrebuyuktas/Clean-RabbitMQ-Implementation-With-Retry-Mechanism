using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Services;
using RabbitMQ.Utils;
using System.Text;

namespace Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IRabbitMQChannelManager _channelManager;
    private readonly IRabbitMQConsumerManager _consumerManager;
    private IModel _channel;
    private int count = 0;
    public Worker(ILogger<Worker> logger, IRabbitMQChannelManager channelManager, IRabbitMQConsumerManager consumerManager)
    {
        _logger = logger;
        _channelManager = channelManager;
        _consumerManager = consumerManager;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _channel = _channelManager.GetChannel();
        _channel.BasicQos(0, 1, false);

        _consumerManager.CreateExchange(_channel, ExchangeObjects.SampleExchange);
        _consumerManager.DeclareQueue(_channel, QueueObjects.SampleQueue);
        _consumerManager.BindQueue(_channel, QueueObjects.SampleQueue, ExchangeObjects.SampleExchange);

        #region DeadLetter
        _consumerManager.CreateExchange(_channel, ExchangeObjects.DeadLetterExchange);
        _consumerManager.DeclareQueue(_channel, QueueObjects.DeadLetterQueue);
        _consumerManager.BindQueue(_channel, QueueObjects.DeadLetterQueue, ExchangeObjects.DeadLetterExchange);
        #endregion

        return base.StartAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += Consumer_Received;

        _channel.BasicConsume(QueueObjects.SampleQueue.Name, false, consumer);
        await Task.CompletedTask;
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
    {
        var content = Encoding.UTF8.GetString(@event.Body.ToArray());
        count++;
        _logger.LogInformation($"Message received {content}");
        if(count == 1)
        {
            // Reject message for test dead letter

            _channel.BasicReject(deliveryTag: @event.DeliveryTag, requeue: false);
        }
        else
        {
            _channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
        }
    }
}