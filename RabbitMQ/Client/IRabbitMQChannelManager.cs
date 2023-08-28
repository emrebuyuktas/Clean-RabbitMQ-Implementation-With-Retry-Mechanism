namespace RabbitMQ.Client;

public interface IRabbitMQChannelManager
{
    IModel GetChannel();
    void Dispose(IModel channel);
}