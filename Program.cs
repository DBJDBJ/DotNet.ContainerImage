IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<sasser.Worker>();
        services.AddHostedService<sasser.SAS>();
    })
    .Build();

host.Run();
