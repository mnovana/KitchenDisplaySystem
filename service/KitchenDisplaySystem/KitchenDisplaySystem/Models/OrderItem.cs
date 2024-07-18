using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.Models
{
    public class OrderItem
    {

        [Required]
        public int FoodId { get; set; }
        public Food? Food { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
