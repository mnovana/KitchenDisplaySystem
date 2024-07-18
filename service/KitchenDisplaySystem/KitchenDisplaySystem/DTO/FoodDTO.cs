using KitchenDisplaySystem.Models;
using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.DTO
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FoodTypeId { get; set; }
        public string FoodTypeName { get; set; }
    }
}
