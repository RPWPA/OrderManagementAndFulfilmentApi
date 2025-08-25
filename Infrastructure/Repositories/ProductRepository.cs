using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<List<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int? categoryId = null, bool includeSubcategories = false)
        {
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                if (includeSubcategories)
                {
                    var childIds = await _context.Categories
                        .Where(c => c.ParentCategoryId == categoryId.Value)
                        .Select(c => c.Id)
                        .ToListAsync();

                    var ids = childIds.Append(categoryId.Value).ToList();
                    query = query.Where(p => ids.Contains(p.CategoryId));
                }
                else
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchAsync(
            string? name, int? categoryId, bool includeSubcategories,
            decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Name.Contains(name));

            if (categoryId.HasValue)
            {
                if (includeSubcategories)
                {
                    var childIds = await _context.Categories
                        .Where(c => c.ParentCategoryId == categoryId.Value)
                        .Select(c => c.Id)
                        .ToListAsync();

                    var ids = childIds.Append(categoryId.Value).ToList();
                    query = query.Where(p => ids.Contains(p.CategoryId));
                }
                else
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                }
            }

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            return await query.ToListAsync();
        }

    }
}
