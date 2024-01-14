using SoftRoboticsAPI.Controllers;
using SoftRoboticsWinService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<SoftRobotics.Business.RandomWordService>();
        services.AddSingleton<ApiRandomWordController>();
    })
    .Build();

await host.RunAsync();
