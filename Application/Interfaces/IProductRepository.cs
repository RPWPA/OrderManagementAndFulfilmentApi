using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);

        // ✅ Search & Filtering
        Task<List<Product>> SearchAsync(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice);
    }
}
