using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorcBooks.DAL;
using TorcBooks.DAL.Entities;

namespace TorcBooks.Integration.Tests.Orchestrator
{
    public class TestOrchestrator
    {
        private static TestOrchestrator _instance;
        public static TestOrchestrator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TestOrchestrator();
                return _instance;
            }
        }
        const string databaseConnectionString = "Server=127.0.0.1,1433;Database=TorcBooks;User Id=sa;Password=Pass@word;Encrypt=False";
        const string rabbitMQConnectionString = "host=127.0.0.1";

        public void SetupRabbitMQConnection()
        {
            Environment.SetEnvironmentVariable("RABBITMQ", rabbitMQConnectionString);
        }

        public TorcDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TorcDbContext>()
                .UseSqlServer(databaseConnectionString).Options;
            return new TorcDbContext(options);
        }

        public void CleanDatabase()
        {
            using (var db = GetDbContext())
            {
                db.Books.ExecuteDelete();
            }
        }

        public async Task AddDummieData()
        {
            List<Books> books = new List<Books> 
            { 
                new Books("Narnia", "C.S.", "Lewis", 20, 8, "Paperback", "08885468", "Fiction"), 
                new Books("Pride and prejudice", "Jane", "Austen", 10, 3, "Paperback", "08822468", "Novel"),
                new Books("Hamlet", "William", "Shakespeare", 40, 23, "Paperback", "18821468", "Drama")
            };
            using (var db = GetDbContext())
            {
                await db.Books.AddRangeAsync(books);
                await db.SaveChangesAsync();
            }
        }

        public HttpClient GetHttpClient()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            var httpCient = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
            }).CreateDefaultClient();
            return httpCient;
        }
    }
}
