using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.Repositories.Interfaces
{
    public interface IFoodTypeRepository
    {
        Task<IEnumerable<FoodType>> GetAllAsync();
    }
}
