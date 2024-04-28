using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using TorcBooks.Data;
using TorcBooks.Data.Entities;
using TorcBooks.Integration.Tests.Infra;
using TorcBooks.Integration.Tests.Models;

namespace TorcBooks.Integration.Tests
{
    public class BooksControllerTests
    {
        public BooksControllerTests()
        {

        }

        [Theory]
        [InlineData("title", "Pride")]
        [InlineData("authors", "Lewis")]
        [InlineData("category", "Fiction")]
        [InlineData("isbn", "1234567892")]
        [InlineData("type", "Paperback")]
        public async Task Get_ShouldReturn200WithResults(string searchBy, string searchValue)
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            var httpCient = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.ConfigureTestServices(TestOrchestrator.ClearDatabase);
            }).CreateDefaultClient();

            var response = await httpCient.GetAsync($"api/books?searchBy={searchBy}&searchValue={searchValue}");
            var jsonResponse = await response.Content.ReadFromJsonAsync<GetBooksResponse[]>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            jsonResponse.Should().NotBeNull();
            jsonResponse.Should().HaveCountGreaterThan(0);
        }

        [Theory]
        [InlineData("title", "TarcisioSouza")]
        [InlineData("authors", "TarcisioSouza1234")]
        [InlineData("category", "TarcisioS178")]
        [InlineData("isbn", "Tarcisio189")]
        [InlineData("type", "TarcisioSouza1098")]
        public async Task Get_ShouldReturn200WithNoResults(string searchBy, string searchValue)
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            var httpCient = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.ConfigureTestServices(TestOrchestrator.ClearDatabase);
            }).CreateDefaultClient();

            var response = await httpCient.GetAsync($"api/books?searchBy={searchBy}&searchValue={searchValue}");
            var jsonResponse = await response.Content.ReadFromJsonAsync<GetBooksResponse[]>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            jsonResponse.Should().NotBeNull();
            jsonResponse.Should().HaveCount(0);
        }
    }
}