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
    }
}
