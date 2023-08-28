namespace RabbitMQ.Models;

public class ExchangeConfig
{
    public string Name { get; set; }
    public string Type { get; set; }
    public bool Durable { get; set; }= true;
    public bool AutoDelete { get; set; }= false;
    public IDictionary<string, object> Arguments { get; set; } = null;
}
