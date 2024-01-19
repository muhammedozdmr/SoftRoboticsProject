using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SoftRobotics.DataAccess;
using SoftRobotics.Domain;
using SoftRobotics.Dto;
using System.Text;

class Program
{
    private static string _queueName = "softRobotics_rabbitQueue";
    private static readonly SoftRoboticsContext _context = new SoftRoboticsContext();

    static void Main(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "127.0.0.1", Port = 5672, UserName = "guest", Password = "guest" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            _queueName = args.Length > 0 ? args[0] : _queueName;
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Mesaj: {message}");
                    var wordDtoList = JsonConvert.DeserializeObject<List<RandomWordDto>>(message);
                    foreach (var wordDto in wordDtoList)
                    {
                        if (wordDto != null)
                        {
                            var wordModel = MapToEntity(wordDto);
                            _context.RandomWords.Add(wordModel);
                        }
                    }
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }

            };
            //Consume etme başlatıldı
            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            Console.WriteLine($"{_queueName} Consumer başlatıldı");
            Console.ReadLine();
        }

    }
    private static RandomWord MapToEntity(RandomWordDto wordDto)
    {
        RandomWord entity = null;
        if (wordDto != null)
        {
            entity = new RandomWord()
            {
                Id = wordDto.Id,
                Word = wordDto.Word,
                CountWord = wordDto.CountWord
            };
        }
        return entity;
    }

}

