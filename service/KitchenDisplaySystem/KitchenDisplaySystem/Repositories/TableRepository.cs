using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KitchenDisplaySystem.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;

        public TableRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _context.Tables
                .Where(t => !t.Deleted)
                .ToListAsync();
        }

        public async Task<Table?> GetByIdAsync(int id)
        {
            return await _context.Tables
                .Where(t => !t.Deleted)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Table table)
        {
            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)    // if someone updated the table after we fetched it
            {
                throw;
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            int rowsAffected = await _context.Tables
                .Where(t => t.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(t => t.Deleted, true));

            return rowsAffected == 1;
        }
    }
}
