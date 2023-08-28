using Consumer;
using RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddRabbitMQServices(hostContext.Configuration);
    })
    .Build();
host.Run();
