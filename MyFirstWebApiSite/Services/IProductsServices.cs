using Entities;

namespace Services
{
    public interface IProductsServices
    {
        Task<List<Product>> GetProducts(float? min, float? max, string? description, int?[] category);
        Task<Product> GetProductById(int id);

    }
}