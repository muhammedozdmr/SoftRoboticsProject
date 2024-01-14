using System.Diagnostics;
using System.Runtime.CompilerServices;
using SoftRoboticsAPI.Controllers;

namespace SoftRoboticsWinService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private readonly SoftRobotics.Business.RandomWordService _randomWordService;
        private readonly ApiRandomWordController _apiRandom;
        public Worker(ILogger<Worker> logger, SoftRobotics.Business.RandomWordService randomWordService,ApiRandomWordController apiRandomWord)
        {
            _logger = logger;
            _randomWordService = randomWordService;
            _apiRandom = apiRandomWord;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //30 saniyede bir çalışacak
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
        private void DoWork(object state)
        {
            try
            {
                //_randomWordService.GenerateWord();
                _apiRandom.Generate();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "çalışma esnasında hata oluştu !");
            }
            _logger.LogInformation("WinService çalışıyor: {time}", DateTimeOffset.Now);
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            await base.StopAsync(stoppingToken);
        }
    }
}
