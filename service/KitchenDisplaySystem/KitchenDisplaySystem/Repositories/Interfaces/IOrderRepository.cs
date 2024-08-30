using KitchenDisplaySystem.DTO.StatsDTOs;
using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(DateTime date);
        Task<Order?> GetByIdAsync(int id);
        Task<Order?> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<bool> UpdateEndTimeAsync(int id, DateTime end);
        Task<bool> UpdateServedAsync(int id);
        Task<IEnumerable<Order>> GetAllUnservedAsync();
        Task<OrdersTodayDTO> GetOrdersToday();
        Task<int> GetAveragePrepareTime();
        Task<IEnumerable<OrdersByMonthDTO>> GetOrdersByMonthAsync(int year);
        Task<IEnumerable<OrdersByWaiterDTO>> GetOrdersByWaiterAsync();
    }
}
