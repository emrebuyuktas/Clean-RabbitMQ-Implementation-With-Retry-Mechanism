namespace RabbitMQ.Client;

public class RabbitMQChannelManager : IRabbitMQChannelManager
{
    private readonly IConnection _connection;

    public RabbitMQChannelManager(IConnection connection)
    {
        _connection = connection;
    }

    public IModel GetChannel()
    {
        return _connection.CreateModel();
    }

    public void Dispose(IModel channel)
    {
        if (channel.IsOpen)
        {
            channel.Close();
            channel.Dispose();
        }
    }
}
