using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KpiPractice.Core.Goods;
using KpiPractice.Orchestrators.Goods;
using Microsoft.AspNetCore.Mvc;

namespace kpi_practice_onion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Good : ControllerBase
    {
        private readonly IGoodService _service;
        private readonly IMapper _mapper;

        public Good(IGoodService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        
        [HttpPost("shops/{shopId}/goods")]
        public async Task<IActionResult> PostAsync(int shopId, KpiPractice.Orchestrators.Goods.Good good)
        {
            var goodModel = _mapper.Map<KpiPractice.Core.Goods.Goods>(good);
            goodModel.ShopId = shopId
                ;
            var createdModel = await _service.AddAsync(goodModel);
            return Ok(_mapper.Map<KpiPractice.Core.Goods.Goods>(createdModel));
        }
        [HttpGet("shops/goods/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var good = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<KpiPractice.Core.Goods.Goods>(good));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCount(int id, [FromBody]UpdateSum sum)
        {
            await _service.Update(id, sum.Sum);
            return Ok((id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveById(id);
            return Ok();
        }

    }
}
