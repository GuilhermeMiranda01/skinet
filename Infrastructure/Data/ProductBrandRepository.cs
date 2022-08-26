using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductBrandRepository : BaseRepository<ProductBrand>, IProductBrandRepository
    {
        public ProductBrandRepository(StoreContext context) : base(context)
        {
        }
    }
}