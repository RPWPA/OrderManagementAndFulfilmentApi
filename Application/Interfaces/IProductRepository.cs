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
        Task<bool> AdjustStockAsync(int productId, int quantityChange);

        // ✅ Search & Filtering
        Task<List<Product>> GetAllAsync(int? categoryId = null, bool includeSubcategories = false);
        Task<List<Product>> SearchAsync(string? name, int? categoryId, bool includeSubcategories, decimal? minPrice, decimal? maxPrice);
    }
}
