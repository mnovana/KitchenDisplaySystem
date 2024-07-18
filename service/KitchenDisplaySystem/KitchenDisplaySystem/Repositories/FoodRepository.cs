using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KitchenDisplaySystem.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly AppDbContext _context;

        public FoodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            return await _context.Food
                .Include(f => f.FoodType)
                .ToListAsync();
        }

        public async Task<Food?> GetByIdAsync(int id)
        {
            return await _context.Food
                .Include(f => f.FoodType)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
