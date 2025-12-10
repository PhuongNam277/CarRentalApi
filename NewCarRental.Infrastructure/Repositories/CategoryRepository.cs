using Microsoft.EntityFrameworkCore;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;
using NewCarRental.Infrastructure.Contexts;

namespace NewCarRental.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CarRentalDbContext _context;
        public CategoryRepository(CarRentalDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category?> AddCategoryAsync(Category category)
        {
            var entry = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
