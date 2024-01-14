using RabbitMQ.Client;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "SA",
    Password = "Gofret-2608"
};

using (var connection = connectionFactory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "RandomWord_Queue",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);
}