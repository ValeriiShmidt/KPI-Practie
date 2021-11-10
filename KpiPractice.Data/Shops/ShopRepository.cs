using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KpiPractice.Core.Shops;
using Microsoft.EntityFrameworkCore;

namespace KpiPractice.Data.Shops
{
    public class ShopRepository : IShopRepository
    {
        private readonly IMapper _mapper;
        private readonly ShopContext _context;

        public ShopRepository(IMapper mapper,
            ShopContext ShopContext)
        {
            _mapper = mapper;
            _context =ShopContext;
        }

        public async Task<Core.Shops.Shop> AddAsync(Core.Shops.Shop shop)
        {
            var daoNew = _mapper.Map<Shop>(shop);
            var addAsync = await _context.Shops.AddAsync(daoNew);
            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Shops.Shop>(addAsync.Entity);
        }
        
        public async Task<Core.Shops.Shop> GetByIdAsync(int id)
        {
            var entity = await _context.Shops.FirstAsync(x => x.Id == id);
            if (entity == null)
                throw new ArgumentNullException();
            return _mapper.Map<Core.Shops.Shop>(entity);
        }

        public async Task RemoveById(int id)
        {
            var entity = await _context.Shops.FirstAsync(x => x.Id == id);
            _context.Shops.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Core.Shops.Shop> Update(int id, int count)
        {
            Shop shop = (
                from n in _context.Shops
                where n.Id == id
                select n).First();
            
            shop.Count = count;
            var addResult = _context.Shops.Update(shop);

            await _context.SaveChangesAsync();
            return _mapper.Map<Core.Shops.Shop>(addResult.Entity);
        }
    }
}
