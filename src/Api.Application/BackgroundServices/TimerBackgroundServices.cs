using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Application.BackgroundServices
{
    public class ImplementBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ImplementBackgroundService> _logger;

        public ImplementBackgroundService(ILogger<ImplementBackgroundService> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Executing PeriodicBackgroundTask");
                _logger.LogError($"Error is occured on {DateTime.UtcNow}!");
                // Console.WriteLine($"Respponse from Background Service - {DateTime.Now}");
                // await Task.Delay(1000);
            }
        }
    }
}