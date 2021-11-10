using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KpiPractice.Core.Shops;
using KpiPractice.Core.Goods;

namespace KpiPractice.Orchestrators.Goods
{
    public class GoodService : IGoodService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IGoodRepository _goodRepository;

        public GoodService(
            IShopRepository shopRepository,
            IGoodRepository goodRepository)
        {
            _shopRepository = shopRepository;
            _goodRepository = goodRepository;
        }
        public async Task<Core.Goods.Goods> GetByIdAsync(int id)
        {
            return await _goodRepository.GetByIdAsync(id);
        }

        public async Task<Core.Goods.Goods> Update(int id, int sum)
        {
            var good = await _goodRepository.GetByIdAsync(id);
            if (good == null)
                throw new ArgumentNullException();
            if (sum < 0)
                throw new ArgumentOutOfRangeException();
            good.Sum = sum;
            await _goodRepository.Update(id, sum);
            return good;
        }

        public async Task RemoveById(int id)
        {
            var good = await _goodRepository.GetByIdAsync(id);
            if (good == null)
                throw new ArgumentNullException();
            await _goodRepository.RemoveById(id);
        }

        public async Task<Core.Goods.Goods> AddAsync(Core.Goods.Goods good)
        {
            var existingShop = await _shopRepository.GetByIdAsync(good.ShopId);
            

            return await _goodRepository.AddAsync(good);
        }
    }
}
