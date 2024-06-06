using Entities;
using Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductsServices : IProductsServices
    {
        private IProductRepository _productRepository;

        public ProductsServices(IProductRepository productsRepository)
        {
            _productRepository = productsRepository;
        }




        public async Task<List<Product>> GetProducts(float? min, float? max, string? description, int?[] category)
        {
            return await _productRepository.GetProducts(min, max, description, category);
        }


        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }
    }
}
