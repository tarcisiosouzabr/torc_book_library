using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using TorcBooks.DAL;
using TorcBooks.DAL.Entities;
using TorcBooks.Integration.Tests.Models;
using TorcBooks.Integration.Tests.Orchestrator;

namespace TorcBooks.Integration.Tests
{
    public class GetBooksControllerTests : IDisposable
    {
        public GetBooksControllerTests()
        {
            TestOrchestrator.Instance.CleanDatabase();
            TestOrchestrator.Instance.AddDummieData().GetAwaiter().GetResult();
        }

        [Theory]
        [InlineData("title", "Narnia")]
        [InlineData("authors", "Austen")]
        [InlineData("category", "Fiction")]
        [InlineData("isbn", "08885468")]
        [InlineData("type", "Paperback")]
        public async Task Get_ShouldReturn200WithResults(string searchBy, string searchValue)
        {
            var httpClient = TestOrchestrator.Instance.GetHttpClient();

            var response = await httpClient.GetAsync($"api/books?searchBy={searchBy}&searchValue={searchValue}");
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
            var httpClient = TestOrchestrator.Instance.GetHttpClient();

            var response = await httpClient.GetAsync($"api/books?searchBy={searchBy}&searchValue={searchValue}");
            var jsonResponse = await response.Content.ReadFromJsonAsync<GetBooksResponse[]>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            jsonResponse.Should().NotBeNull();
            jsonResponse.Should().HaveCount(0);
        }

        public void Dispose()
        {
            TestOrchestrator.Instance.CleanDatabase();
        }
    }
}