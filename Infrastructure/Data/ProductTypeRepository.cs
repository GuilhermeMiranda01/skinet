using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductTypeRepository : BaseRepository<ProductType>, IProductTypeRepository
    {
        private readonly StoreContext _context;
        public ProductTypeRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

    }
}