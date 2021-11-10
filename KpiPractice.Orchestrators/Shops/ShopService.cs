using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KpiPractice.Core.Shops;

namespace KpiPractice.Orchestrators.Shops
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository ;
        }
        public async Task<Core.Shops.Shop> AddAsync(Core.Shops.Shop shop)
        {
            return await _shopRepository.AddAsync(shop); 
        }

        public async Task<Core.Shops.Shop> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return await _shopRepository.GetByIdAsync(id);
        }
        public async Task<Core.Shops.Shop> Update(int id, int count)
        {
            var bank = await _shopRepository.GetByIdAsync(id);
            if (bank == null)
                throw new ArgumentNullException();
            bank.Count = count;
            var updateShop = await _shopRepository.Update(id, count);
            return updateShop;
        }
        public async Task RemoveById(int id)
        {
            var shop = await _shopRepository.GetByIdAsync(id);
            if (shop == null)
                throw new ArgumentOutOfRangeException();
            await _shopRepository.RemoveById(id);
        }
    }
}
