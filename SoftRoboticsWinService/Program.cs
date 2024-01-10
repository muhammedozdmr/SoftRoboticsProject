using SoftRoboticsWinService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<SoftRobotics.Business.RandomWordService>();
    })
    .Build();

await host.RunAsync();
