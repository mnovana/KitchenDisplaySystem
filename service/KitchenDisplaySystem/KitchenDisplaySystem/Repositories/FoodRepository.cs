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
                .Where(f => !f.Deleted)
                .Include(f => f.FoodType)
                .ToListAsync();
        }

        public async Task<Food?> GetByIdAsync(int id)
        {
            return await _context.Food
                .Where(f => !f.Deleted)
                .Include(f => f.FoodType)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Food food)
        {
            await _context.Food.AddAsync(food);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)    // if foreign key doesn't exist etc.
            {
                throw;
            }
        }

        public async Task UpdateAsync(Food food)
        {
            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await _context.Food
                .Where(f => f.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(f => f.Deleted, true));

            return rowsAffected == 1;
        }
    }
}
