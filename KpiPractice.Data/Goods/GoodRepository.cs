using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KpiPractice.Core.Goods;
using Microsoft.EntityFrameworkCore;

namespace KpiPractice.Data.Goods
{
    public class GoodRepository : IGoodRepository
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _context;

        public GoodRepository(IMapper mapper, ShopContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<global::KpiPractice.Core.Goods.Goods> GetByIdAsync(int id)
        {
            var entity = await _context.Goods.FirstAsync(x => x.Id == id);
            return _mapper.Map<global::KpiPractice.Core.Goods.Goods>(entity);
        }

        public async Task<global::KpiPractice.Core.Goods.Goods> Update(int id, int sum)
        {
            Good goods = (
                from n in _context.Goods
                where n.Id == id
                select n).First();
            
            goods.Sum = sum;
            var addResult = _context.Goods.Update(goods);

            await _context.SaveChangesAsync();
            return _mapper.Map<global::KpiPractice.Core.Goods.Goods>(goods);
            
        }

        public async Task RemoveById(int id)
        {
            var entity = await _context.Goods.FirstAsync(x => x.Id == id);
            _context.Goods.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<global::KpiPractice.Core.Goods.Goods> AddAsync(global::KpiPractice.Core.Goods.Goods good)
        {
            var goodEntity = _mapper.Map<Good>(good);
            var result = await _context.Goods.AddAsync(goodEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<global::KpiPractice.Core.Goods.Goods>(result.Entity);
            
        }
    }
}
