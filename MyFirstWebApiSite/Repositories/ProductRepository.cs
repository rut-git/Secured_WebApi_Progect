using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private AdoNetUsers326077351Context _ProductContext;

        public ProductRepository(AdoNetUsers326077351Context productContext)
        {
            _ProductContext = productContext;
        }

        /*float? min, float? max, List<Category> category, string? description*/
        public async Task<List<Product>> GetProducts(float? min, float? max,  string? description, int?[] categoryId)
        {
            var query = _ProductContext.Products.Where(product =>
            (description == null ? (true) : (product.ProductName.Contains(description)))
            && ((min == null) ? (true) : (product.Price >= min))
            && ((max == null) ? (true) : (product.Price <= max))
            && ((categoryId.Length == 0) ? (true) : (categoryId.Contains(product.CategoryId))))
                .OrderBy(Product => Product.Price);

            List<Product> products = await query.ToListAsync();
            return products;

           // return await _ProductContext.Products.Include(c=>c.Category).ToListAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _ProductContext.Products.FindAsync(id);
        }

    }
}
