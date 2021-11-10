using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiPractice.Core.Shops
{
    public interface IShopService
    {
        Task<Shop> GetByIdAsync(int id);
        Task<Shop> Update(int id, int count);
        Task RemoveById(int id);
        Task<Shop> AddAsync(Shop shop);
    }
}
