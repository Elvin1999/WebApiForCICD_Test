using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using WebApiForCICD;

namespace WebApi.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            });

            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetProducts_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
