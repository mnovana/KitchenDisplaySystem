using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }


        [Required]
        public int FoodTypeId { get; set; }
        public FoodType? FoodType { get; set; }
    }
}
