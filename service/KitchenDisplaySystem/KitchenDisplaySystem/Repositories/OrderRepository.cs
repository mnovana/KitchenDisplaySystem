using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KitchenDisplaySystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Order?> AddAsync(Order order)
        {
            // EF will also insert every OrderItem
            await _context.Orders.AddAsync(order);

            // SaveChangesAsync() will assign an ID to the new order object
            await _context.SaveChangesAsync();

            if (order.Id == 0)
            {
                return null;
            }

            return await GetByIdAsync(order.Id);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllUnservedAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
                .Where(o => !o.Served).ToListAsync();
        }

        public async Task UpdateEndTimeAsync(int id, DateTime end)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(o => o.End, end));
        }

        public async Task<bool> UpdateServedAsync(int id)
        {
            // order can't be served it's not prepared yet
            int rowsAffected = await _context.Orders
                .Where(o => o.Id == id)
                .Where(o => o.End != null)
                .ExecuteUpdateAsync(x => x.SetProperty(o => o.Served, true));

            return rowsAffected > 0;
        }
    }
}
