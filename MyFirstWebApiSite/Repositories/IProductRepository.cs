using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(float? min, float? max, string? description, int?[] categoryId);
        Task<Product> GetProductById(int id);

    }
}