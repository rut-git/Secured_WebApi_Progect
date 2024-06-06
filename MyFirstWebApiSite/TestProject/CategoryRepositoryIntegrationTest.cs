using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class CategoryRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {

        private readonly AdoNetUsers326077351Context _dbContext;
        private readonly CategoriesRepository _categoryRepository;

        public CategoryRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _categoryRepository = new CategoriesRepository(_dbContext);
        }

        [Fact]
        public async Task GetAllCategories_ReturnsAllCategories()
        {
            // Arrange
            var category1 = new Category { CategoryName = "Category1" };
            var category2 = new Category { CategoryName = "Category2" };
            await _dbContext.Categories.AddRangeAsync(category1, category2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.CategoryName == "Category1");
            Assert.Contains(result, c => c.CategoryName == "Category2");
        }
    }
}
