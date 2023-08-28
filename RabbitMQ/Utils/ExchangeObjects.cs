using RabbitMQ.Client;
using RabbitMQ.Constants;
using RabbitMQ.Models;

namespace RabbitMQ.Utils;

public static class ExchangeObjects
{
    public static readonly ExchangeConfig SampleExchange = new()
    {
        Name = ExchangeNames.SampleExchange,
        Type = ExchangeType.Direct,
        Durable = true,
        AutoDelete = false,
        Arguments = null
    };

    public static readonly ExchangeConfig DeadLetterExchange = new()
    {
        Name = ExchangeNames.SampleDeadLetterExchange,
        Type = ExchangeType.Fanout,
        Durable = true,
        AutoDelete = false,
        Arguments = null
    };
}
