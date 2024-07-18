using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem orderItem);
    }
}
