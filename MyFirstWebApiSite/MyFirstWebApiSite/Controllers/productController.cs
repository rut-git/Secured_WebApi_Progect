using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

    namespace MyFirstWebApiSite.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class productController : ControllerBase
        {
            private IProductsServices _productService;
            private IMapper _Mapper;

            public productController(IProductsServices productService , IMapper Mapper)
            {
                 _productService = productService;
                 _Mapper = Mapper;
            }
            [HttpGet]
            public async Task<ActionResult<List<productDTO>>> Get([FromQuery] float? min, [FromQuery] float? max, [FromQuery] string? description, [FromQuery] int?[] category)
            {
                List<Product> products = await _productService.GetProducts(min, max, description, category);
                List<productDTO> productsDTO = _Mapper.Map<List<Product>, List<productDTO>>(products);
                if (productsDTO == null)
                    return NoContent();
                return Ok(productsDTO);
            }
        }
    }
