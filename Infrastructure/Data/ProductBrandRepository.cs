using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductBrandRepository : IBaseRepository<ProductBrand>, IProductBrandRepository
    {
        private readonly StoreContext _context;
        public ProductBrandRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<ProductBrand>> GetAllAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<ProductBrand> GetByIdAsync(int id)
        {
            return await _context.ProductBrands.FindAsync(id);
        }

    }
}