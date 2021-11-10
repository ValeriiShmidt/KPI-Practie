using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KpiPractice.Core.Goods
{
    public interface IGoodService
    {
        Task<Goods> GetByIdAsync(int id);
        Task<Goods> Update(int id, int sum);
        Task RemoveById(int id);
        Task<Goods> AddAsync(Goods goods);
    }
}
