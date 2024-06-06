using Entities;

namespace Services
{
    public interface ICategoriesServices
    {
        Task<List<Category>> GetCategories();
    }
}