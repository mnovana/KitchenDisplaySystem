using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IWaiterRepository
    {
        Task<IEnumerable<Waiter>> GetAllAsync();
    }
}
