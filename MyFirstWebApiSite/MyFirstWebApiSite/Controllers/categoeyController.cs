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
    public class categoeyController : ControllerBase
    {
        private ICategoriesServices _categoryService;
        private IMapper _mapper;

        public categoeyController(ICategoriesServices categoryServices, IMapper mapper)
        {
            _categoryService = categoryServices;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            List<Category> categories = await _categoryService.GetCategories();
            List<categoryDTO> categoryDTO = _mapper.Map<List<Category>, List<categoryDTO>>(categories);
            if (categories == null)
                return NoContent();
            return Ok(categories);
        }

    }
}
