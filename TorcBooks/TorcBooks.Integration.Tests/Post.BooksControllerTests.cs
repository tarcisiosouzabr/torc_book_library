using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TorcBooks.Integration.Tests.Orchestrator;

namespace TorcBooks.Integration.Tests
{
    public class PostBooksControllerTests : IDisposable
    {
        public PostBooksControllerTests()
        {
            TestOrchestrator.Instance.CleanDatabase();
            TestOrchestrator.Instance.SetupRabbitMQConnection();
        }

        [Fact]
        public async Task Post_BooksControllerTests_ShouldReturn200()
        {
            var httpClient = TestOrchestrator.Instance.GetHttpClient();
            var request = new CreateBookEvent 
            { 
                Category = "Fiction",
                CopiesInUse = 10,
                FirstName = "C.S.",
                ISBN = "8864595",
                LastName = "Lewis",
                Title = "Narnia",
                TotalCopies = 8,
                Type = "Test"
            };
            var dbContext = TestOrchestrator.Instance.GetDbContext();

            var response = await httpClient.PostAsJsonAsync("api/books", request);
            var books = await dbContext.Books.CountAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            books.Should().BeGreaterThan(0);

            dbContext.Dispose();
        }

        [Fact]
        public async Task Post_BooksControllerTests_ShouldReturn400()
        {
            var httpClient = TestOrchestrator.Instance.GetHttpClient();
            var request = new CreateBookEvent
            {
                Category = null,
                CopiesInUse = 10,
                FirstName = "C.S.",
                ISBN = null,
                LastName = null,
                Title = "Narnia",
                TotalCopies = 8,
                Type = "Test"
            };
            var dbContext = TestOrchestrator.Instance.GetDbContext();

            var response = await httpClient.PostAsJsonAsync("api/books", request);
            var books = await dbContext.Books.CountAsync();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            books.Should().Be(0);

            dbContext.Dispose();
        }

        public void Dispose()
        {
            TestOrchestrator.Instance.CleanDatabase();
        }
    }
}
