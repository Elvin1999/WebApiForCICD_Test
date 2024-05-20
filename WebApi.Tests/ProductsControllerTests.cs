using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;
using WebApiForCICD;
using WebApiForCICD.Entities;

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
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task CreateProduct_ReturnsCreatedResponse()
        {
            var product = new Product { Name = "Test Product", Price = 9.99 };
            var response = await _client.PostAsJsonAsync("/api/products", product);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.IsNotNull(createdProduct);
            Assert.AreEqual("Test Product", createdProduct.Name);
        }

    }
}
