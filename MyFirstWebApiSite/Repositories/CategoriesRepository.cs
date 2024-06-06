using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private AdoNetUsers326077351Context _CategoriesContext;

        public CategoriesRepository(AdoNetUsers326077351Context CategoriesContext)
        {
            _CategoriesContext = CategoriesContext;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _CategoriesContext.Categories.ToListAsync();
        }
    }
}
