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
            return await _context.FoodTypes.ToListAsync();
        }

        public async Task<FoodType?> GetByIdAsync(int id)
        {
            return await _context.FoodTypes
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

        public async Task DeleteAsync(FoodType foodType)
        {
            _context.FoodTypes.Remove(foodType);
            await _context.SaveChangesAsync();
        }
    }
}
