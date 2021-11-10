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
    public class ClientTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUrl = "/api/Client/banks/clients/";

        [SetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        [Test]
        public async Task goodsController_GetById_ReturnsGoodModel()
        {
            var httpResponse = await _client.GetAsync(RequestUrl + 1);
            
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var good = JsonConvert.DeserializeObject<KpiPractice.Orchestrators.Goods.Good>(stringResponse);
            
            Assert.AreEqual(1, good.Id);
            Assert.AreEqual("asdfds", good.Name);
            Assert.AreEqual("fdsd", good.Brand);
            Assert.AreEqual(1234, good.Price);
            Assert.AreEqual("asdf", good.Category);
            Assert.AreEqual("dadsf", good.Description);
            Assert.AreEqual(332412, good.Sum);
        }
        [Test]
        public async Task goodsController_Add_AddsGoodToDatabase()
        {
            var good = new KpiPractice.Orchestrators.Goods.Good(){ Name = "asdfas", Brand = "dsfa", Price = 1234123, Category = "asdf", Description = "asdf", Sum = 124242};
            var content = new StringContent(JsonConvert.SerializeObject(good), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync($"/api/Good/shops/{1}/goods", content);

            
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var clientInResponse = JsonConvert.DeserializeObject<KpiPractice.Orchestrators.Goods.Good>(stringResponse);

            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<ShopContext>();
                var databasegood = await context.Goods.FindAsync(clientInResponse.Id);
                Assert.AreEqual(1, good.Id);
                Assert.AreEqual("asdfds", good.Name);
                Assert.AreEqual("fdsd", good.Brand);
                Assert.AreEqual(1234, good.Price);
                Assert.AreEqual("asdf", good.Category);
                Assert.AreEqual("dadsf", good.Description);
                Assert.AreEqual(332412, good.Sum);
            }
        }
        [Test]
        public async Task goodsController_Update_UpdatesGoodInDatabase()
        {
            var good = new KpiPractice.Orchestrators.Goods.Good{Id = 1, Sum = 1843};
            var content = new StringContent(await JsonConvert.SerializeObjectAsync(good), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PatchAsync($"/api/good/{good.Id}", content);

            httpResponse.EnsureSuccessStatusCode();
            
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<ShopContext>();
                var databasegood = await context.Goods.FindAsync(good.Id);
                Assert.AreEqual(good.Sum, databasegood.Sum);
            }
        }
        [Test]
        public async Task GoodsController_DeleteById_DeletesGoodFromDatabase()
        {
            var goodId = 1;
            var httpResponse = await _client.DeleteAsync("api/Good/" + goodId);

            httpResponse.EnsureSuccessStatusCode();
            
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<ShopContext>();
                
                Assert.AreEqual(0, context.Goods.Count());
            }
        }
    }
}