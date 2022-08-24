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
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProduct(int id)
        {
            return Ok(await _productTypeRepository.GetByIdAsync(id));
        }
    }
}