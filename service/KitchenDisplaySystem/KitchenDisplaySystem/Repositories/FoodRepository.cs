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

        public async Task AddAsync(Food food)
        {
            await _context.Food.AddAsync(food);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Food food)
        {
            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)    // if someone updated the food after we fetched it
            {
                throw;
            }

        }

        public async Task DeleteAsync(Food food)
        {
            _context.Food.Remove(food);
            await _context.SaveChangesAsync();
        }
    }
}
