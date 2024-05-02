using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TorcBooks.DAL;
using TorcBooks.DAL.Entities;
using TorcBooks.Integration;

namespace TorcBooks.Store.BackgroundServices
{
    public class NewBookEventHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public NewBookEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                string? rabbitConnection = Environment.GetEnvironmentVariable("RABBITMQ");
                using (var bus = RabbitHutch.CreateBus(rabbitConnection))
                {
                    Console.WriteLine("Registration process started");
                    bus.PubSub.Subscribe<CreateBookEvent>("BookCreate", ProcessBookIntegration);
                    Console.WriteLine("Registration process finished");
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                        Console.WriteLine("Registration is alive");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private async Task ProcessBookIntegration(CreateBookEvent integrationEvent)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<TorcDbContext>();

                var book = new Books
                    (integrationEvent.Title,
                    integrationEvent.FirstName,
                    integrationEvent.LastName,
                    integrationEvent.TotalCopies,
                    integrationEvent.CopiesInUse,
                    integrationEvent.Type,
                    integrationEvent.ISBN,
                    integrationEvent.Category);
                dbContext.Books.Add(book);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
