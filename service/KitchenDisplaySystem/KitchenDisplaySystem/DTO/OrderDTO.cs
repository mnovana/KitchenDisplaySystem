using KitchenDisplaySystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Note { get; set; }
        public bool Served { get; set; }

        public ICollection<OrderItemDTO> OrderItems { get; set; }

        public int TableNumber { get; set; }
        public string WaiterDisplayName { get; set; }
    }
}
