using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> AddAsync(Order order);
        Task UpdateEndTimeAsync(int id, DateTime end);
        Task<bool> UpdateServedAsync(int id);
        Task<IEnumerable<Order>> GetAllUnservedAsync();
    }
}
