using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private ICategoriesRepository _categoriesRepository;

        public CategoriesServices(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        
   
        public async Task<List<Category>> GetCategories()
        {
            return await _categoriesRepository.GetCategories();
        }
   }
}
