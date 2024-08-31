using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.Models
{
    public class FoodType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        public bool Deleted { get; set; }
    }
}
