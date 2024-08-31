using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IFoodTypeRepository
    {
        Task<IEnumerable<FoodType>> GetAllAsync();
        Task<FoodType?> GetByIdAsync(int id);
        Task AddAsync(FoodType foodType);
        Task UpdateAsync(FoodType foodType);
        Task<bool> DeleteAsync(int id);
    }
}
