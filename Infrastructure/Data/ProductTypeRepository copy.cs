using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductTypeRepository : IBaseRepository<ProductType>, IProductTypeRepository
    {
        private readonly StoreContext _context;
        public ProductTypeRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<ProductType>> GetAllAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> GetByIdAsync(int id)
        {
            return await _context.ProductTypes.FindAsync(id);
        }

    }
}