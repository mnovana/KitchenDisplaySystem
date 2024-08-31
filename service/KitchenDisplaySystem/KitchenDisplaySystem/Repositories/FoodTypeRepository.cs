using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KitchenDisplaySystem.Repositories
{
    public class FoodTypeRepository : IFoodTypeRepository
    {
        private readonly AppDbContext _context;

        public FoodTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoodType>> GetAllAsync()
        {
            return await _context.FoodTypes
                .Where(f => !f.Deleted)
                .ToListAsync();
        }

        public async Task<FoodType?> GetByIdAsync(int id)
        {
            return await _context.FoodTypes
                .Where(f => !f.Deleted)
                .FirstOrDefaultAsync(ft => ft.Id == id);
        }

        public async Task AddAsync(FoodType foodType)
        {
            await _context.FoodTypes.AddAsync(foodType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FoodType foodType)
        {
            _context.Entry(foodType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)    // if someone updated the food type after we fetched it
            {
                throw;
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await _context.FoodTypes
                .Where(f => f.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(f => f.Deleted, true));

            return rowsAffected == 1;
        }
    }
}
