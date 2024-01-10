using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SoftRoboticsWinService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private readonly SoftRobotics.Business.RandomWordService _randomWordService;
        public Worker(ILogger<Worker> logger, SoftRobotics.Business.RandomWordService randomWordService)
        {
            _logger = logger;
            _randomWordService = randomWordService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //30 saniyede bir �al��acak
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
                _randomWordService.GenerateWord();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "�al��ma esnas�nda hata olu�tu !");
            }
            _logger.LogInformation("WinService �al���yor: {time}", DateTimeOffset.Now);
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            await base.StopAsync(stoppingToken);
        }
    }
}
