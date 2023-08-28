using RabbitMQ.Constants;
using RabbitMQ.Models;

namespace RabbitMQ.Utils;

public static class QueueObjects
{
    public static readonly QueueConfig SampleQueue = new()
    {
        Name = QueueNames.SampleQueue,
        RoutingKey = RoutingKeys.SampleRoutingKey,
        Durable = true,
        Exclusive = false,
        AutoDelete = false,
        Arguments = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", ExchangeNames.SampleDeadLetterExchange },
            { "x-dead-letter-routing-key", RoutingKeys.SampleDeadLetterRoutingKey }
        }
    };

    public static readonly QueueConfig DeadLetterQueue = new()
    {
        Name = QueueNames.SampleDeadLetterQueue,
        RoutingKey = RoutingKeys.SampleDeadLetterRoutingKey,
        Durable = true,
        Exclusive = false,
        AutoDelete = false,
        Arguments = null
    };
}