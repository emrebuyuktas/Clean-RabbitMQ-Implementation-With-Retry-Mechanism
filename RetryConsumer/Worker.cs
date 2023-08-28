using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQ.Services;
using RabbitMQ.Utils;
using System.Text;

namespace RetryConsumer
{
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

            _channel.BasicConsume(QueueObjects.DeadLetterQueue.Name, false, consumer);
            await Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                var content = Encoding.UTF8.GetString(@event.Body.ToArray());
                count++;
                _logger.LogInformation($"Message received {content}");
                _channel.BasicAck(deliveryTag: @event.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                _channel.BasicReject(deliveryTag: @event.DeliveryTag, requeue: false);
            }

        }
    }
}