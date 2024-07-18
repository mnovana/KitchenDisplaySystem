using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.DTO
{
    public class OrderDataDTO
    {
        public IEnumerable<FoodDTO> Food { get; set; }
        public IEnumerable<FoodType> FoodTypes { get; set; }
        public IEnumerable<Table> Tables { get; set; }
        public IEnumerable<WaiterDTO> Waiters { get; set; }
    }
}
