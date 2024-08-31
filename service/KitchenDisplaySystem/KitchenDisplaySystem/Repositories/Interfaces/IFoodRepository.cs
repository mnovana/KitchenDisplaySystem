using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAllAsync();
        Task<Food?> GetByIdAsync(int id);
        Task AddAsync(Food food);
        Task UpdateAsync(Food food);
        Task<bool> DeleteAsync(int id);
    }
}
