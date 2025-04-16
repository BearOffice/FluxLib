using Demo.Views;
using Demo.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateApplicationBuilder(args);

host.Services.AddHostedService<Worker>();


host.Services.AddSingleton<DispatchCenter>();

host.Services.AddTransient<SimpleStore>();
host.Services.AddTransient<SimpleView>();

await host.Build().RunAsync();


public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = _serviceProvider.GetRequiredService<SimpleView>();

        await Task.CompletedTask;
    }
}