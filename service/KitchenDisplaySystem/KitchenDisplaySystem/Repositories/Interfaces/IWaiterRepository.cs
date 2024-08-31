using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IWaiterRepository
    {
        Task<IEnumerable<Waiter>> GetAllAsync();
        Task<Waiter?> GetByIdAsync(int id);
        Task AddAsync(Waiter waiter);
        Task UpdateAsync(Waiter waiter);
        Task<bool> DeleteAsync(int id);
    }
}
