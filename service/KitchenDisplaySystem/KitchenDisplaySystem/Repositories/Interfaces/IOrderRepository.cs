using KitchenDisplaySystem.DTO.StatsDTOs;
using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(DateTime date);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> AddAsync(Order order);
        Task UpdateEndTimeAsync(int id, DateTime end);
        Task<bool> UpdateServedAsync(int id);
        Task<IEnumerable<Order>> GetAllUnservedAsync();
        StatsDTO GetStatistics();
        Task<IEnumerable<OrdersByMonthDTO>> GetOrdersByMonthAsync(int year);
        Task<IEnumerable<OrdersByWaiterDTO>> GetOrdersByWaiterAsync();
    }
}
