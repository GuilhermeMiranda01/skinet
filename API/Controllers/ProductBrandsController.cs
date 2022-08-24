using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    //ApiController faz o controle das rotas, e adiciona algumas validações, como por exemplo lança o bad request(400) se alguém
    //tentar passar uma string como parametro do método GetProduct - tem outras coisas mas por enquanto só isso...-
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBrandsController : ControllerBase
    {
        private readonly IProductBrandRepository _productBrandRepository;

        public ProductBrandsController(IProductBrandRepository productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetProduct(int id)
        {
            return Ok(await _productBrandRepository.GetByIdAsync(id));
        }
    }
}