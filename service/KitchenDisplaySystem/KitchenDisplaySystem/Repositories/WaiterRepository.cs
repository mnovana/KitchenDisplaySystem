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

        public async Task AddAsync(Waiter waiter)
        {
            await _context.Waiters.AddAsync(waiter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Waiter waiter)
        {
            _context.Entry(waiter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)    // if someone updated the waiter after we fetched it
            {
                throw;
            }

        }

        public async Task DeleteAsync(Waiter waiter)
        {
            _context.Waiters.Remove(waiter);
            await _context.SaveChangesAsync();
        }
    }
}
