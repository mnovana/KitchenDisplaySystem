using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;

namespace KitchenDisplaySystem.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
