using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KitchenDisplaySystem.Repositories
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly AppDbContext _context;

        public WaiterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Waiter>> GetAllAsync()
        {
            return await _context.Waiters.ToListAsync();
        }

        public async Task<Waiter?> GetByIdAsync(int id)
        {
            return await _context.Waiters
                .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}
