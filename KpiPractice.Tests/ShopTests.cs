using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KpiPractice.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace KpiPractice.Tests
{
    [TestFixture]
    public class ShopIntegrationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUrl = "api/Shop/";

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        [Test]
        public async Task shopsController_GetById_ReturnsShopModel()
        {
            var httpResponse = await _client.GetAsync(RequestUrl + 1);
            
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var shop = JsonConvert.DeserializeObject<Orchestrators.Shops.Shop>(stringResponse);
            
            Assert.AreEqual(1, shop.Id);
            Assert.AreEqual(11231, shop.Count);
            Assert.AreEqual("Де сь", shop.Address);
        }
        [Test]
        public async Task ShopsController_Add_AddsShopToDatabase()
        {
            var shop = new Orchestrators.Shops.Shop(){Address = "asdfasd ssd", Count = 123};
            var content = new StringContent(JsonConvert.SerializeObject(shop), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUrl + 1, content);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var shopInResponse = JsonConvert.DeserializeObject<Orchestrators.Shops.Shop>(stringResponse);

            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<ShopContext>();
                var databaseShop = await context.Shops.FindAsync(shopInResponse.Id);
                Assert.AreEqual(databaseShop.Id, shopInResponse.Id);
                Assert.AreEqual(databaseShop.Count, shopInResponse.Count);
                Assert.AreEqual(databaseShop.Address, shopInResponse.Address);
            }
        }
        [Test]
        public async Task shopsController_Update_UpdatesShopInDatabase()
        {
            var shop = new Orchestrators.Shops.Shop{Id = 1, Count = 1843};
            var content = new StringContent(await JsonConvert.SerializeObjectAsync(shop), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PatchAsync($"/api/Shop/{shop.Id}", content);

            httpResponse.EnsureSuccessStatusCode();
            
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<ShopContext>();
                var databaseshop = await context.Shops.FindAsync(shop.Id);
                Assert.AreEqual(shop.Id, databaseshop.Id);
            }
        }
        [Test]
        public async Task ShopController_DeleteById_DeletesShopFromDatabase()
        {
            var shopId = 1;
            var httpResponse = await _client.DeleteAsync(RequestUrl + shopId);

            httpResponse.EnsureSuccessStatusCode();
            
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<ShopContext>();
                
                Assert.AreEqual(0, context.Shops.Count());
            }
        }
    }
    
    
}