using KitchenDisplaySystem.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace KitchenDisplaySystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [DateInRange]
        public DateTime Start { get; set; }
        [DateInRange]
        public DateTime? End { get; set; }
        [StringLength(500)]
        public string? Note { get; set; }
        [Required]
        public bool Served { get; set; }


        [Required]
        public int TableId { get; set; }
        public Table? Table { get; set; }
        [Required]
        public int WaiterId { get; set; }
        public Waiter? Waiter { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
