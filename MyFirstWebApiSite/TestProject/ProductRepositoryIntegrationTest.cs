using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ProductRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetUsers326077351Context _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _productRepository = new ProductRepository(_dbContext);
        }


        [Fact]
        public async Task GetAllProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var category1 = new Category { CategoryName = "Category1" };
            var category2 = new Category { CategoryName = "Category2" };
            await _dbContext.Categories.AddRangeAsync(category1, category2);
            await _dbContext.SaveChangesAsync();

            var product1 = new Product { ProductName = "Product 1", Description = "Product 1 Description", Price = 10, CategoryId = category1.CategoryId, imageUrl = "1.jpg" };
            var product2 = new Product { ProductName = "Product 2", Description = "Product 2 Description", Price = 20, CategoryId = category2.CategoryId, imageUrl = "2.jpg" };
            await _dbContext.Products.AddRangeAsync(product1, product2);
            await _dbContext.SaveChangesAsync();

            float? minPrice = 5;
            float? maxPrice = 15;
            int?[] categories = { category1.CategoryId };
            string description = "Product 1";

            // Act
            var result = await _productRepository.GetProducts(minPrice, maxPrice, description, categories);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(product1.Description, result.First().Description);
        }

        [Fact]
        public async Task GetProductById_ExistingProductId_ReturnsProduct()
        {
            // Arrange
            var category = new Category { CategoryName = "Category" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var product = new Product { ProductName = "Product 1", Description = "Product", Price = 15, CategoryId = category.CategoryId, imageUrl = "1.jpg" };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            int productId = product.ProductId;

            // Act
            var result = await _productRepository.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
        }
    }
}
