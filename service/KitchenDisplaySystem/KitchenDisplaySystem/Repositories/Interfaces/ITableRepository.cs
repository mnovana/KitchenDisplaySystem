using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllAsync();
        Task<Table?> GetByIdAsync(int id);
    }
}
