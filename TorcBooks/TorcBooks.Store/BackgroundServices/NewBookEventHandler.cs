using EasyNetQ;
using TorcBooks.Integration;

namespace TorcBooks.Store.BackgroundServices
{
    public class NewBookEventHandler : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string? rabbitConnection = Environment.GetEnvironmentVariable("RABBITMQ");
            using (var bus = RabbitHutch.CreateBus(rabbitConnection))
            {
                bus.PubSub.Subscribe<CreateBookEvent>("BookCreate", ProcessBookIntegration);
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                }
            }
        }

        private void ProcessBookIntegration(CreateBookEvent integrationEvent)
        {

        }
    }
}
