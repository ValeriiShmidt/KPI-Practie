using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KpiPractice.Core.Shops;
using KpiPractice.Orchestrators.Shops;
using Microsoft.AspNetCore.Mvc;

namespace kpi_practice_onion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _service;
        private readonly IMapper _mapper;

        public ShopController(IMapper mapper, IShopService service)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var shop = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<KpiPractice.Orchestrators.Shops.Shop>(shop));
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> PostAsync(KpiPractice.Orchestrators.Shops.Shop shop)
        {
            var shopModel = _mapper.Map<KpiPractice.Core.Shops.Shop>(shop);
            var createdModel = await _service.AddAsync(shopModel);
            return Ok(_mapper.Map<KpiPractice.Orchestrators.Shops.Shop>(createdModel));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCount(int id, UpdateCount count)
        {
            await _service.Update(id, count.Count);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.RemoveById(id);
            return Ok();
        }
    }
}
