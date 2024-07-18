using KitchenDisplaySystem.Models;
using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.DTO
{
    public class OrderItemDTO
    {
        public string FoodName { get; set; }
        public int Quantity { get; set; }
    }
}
